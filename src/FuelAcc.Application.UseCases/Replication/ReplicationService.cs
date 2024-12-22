using AutoMapper;
using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.Dto.Replication;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Persistence;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Domain.Entities.Other;
using MediatR;

namespace FuelAcc.Application.UseCases.Replication
{
    public class ReplicationService : IReplicationService
    {
        private readonly IReplicationRepository _replicationRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventConverter _eventConverter;
        private readonly IExecutionContext _executionContext;
        private readonly IMediator _mediator;
        private readonly IReplicationHelper _replicationHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReplicationService(
            IReplicationRepository replicationRepository,
            IEventStoreRepository eventStoreRepository,
            IEventConverter eventConverter,
            IExecutionContext executionContext,
            IMediator mediator,
            IReplicationHelper replicationHelper,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _replicationRepository = replicationRepository;
            _eventStoreRepository = eventStoreRepository;
            _eventConverter = eventConverter;
            _executionContext = executionContext;
            _mediator = mediator;
            _replicationHelper = replicationHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ApplyInboundZipAsync(byte[] compressed, CancellationToken cancellationToken)
        {
            var jws = _replicationHelper.Decompress(compressed);

            var dto = _replicationHelper.Deserialize(jws);

            await ApplyInboundPacketAsync(dto, cancellationToken);
        }

        public async Task ApplyInboundPacketAsync(ReplictionPacketDto packetDto, CancellationToken cancellationToken)
        {
            _executionContext.IsReplicationApplying = true;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var exist = await _replicationRepository.GetAsync(packetDto.Id, cancellationToken);
            if (exist != null)
            {
                throw new DomainException($"The packet with id {packetDto.Id} already applied!");
            }

            if (packetDto.PreviousId.HasValue)
            {
                var prev = await _replicationRepository.GetAsync(packetDto.PreviousId.Value, cancellationToken);
                if (prev == null)
                {
                    throw new DomainException($"Has no prevoius replication packets with id {packetDto.PreviousId.Value}");
                }
            }
            else
            {
                var last = await _replicationRepository.GetLastAsync(packetDto.BranchId, false, cancellationToken);
                if (last != null)
                {
                    throw new DomainException($"Has prevoius replication packet for incoming with empty previous id");
                }
            }

            var currentBranchId = await _replicationRepository.GetCurretBranchAsync(cancellationToken);

            if (packetDto.SourceBranchId == currentBranchId)
            {
                throw new DomainException($"Can't apply own replication packet to self");
            }
            if (packetDto.BranchId != currentBranchId)
            {
                throw new DomainException($"Can't apply replication packet for other branch to self");
            }

            var pkt = _mapper.Map<ReplictionPacket>(packetDto);
            pkt.Outbound = false;

            await _replicationRepository.InsertAsync(pkt, cancellationToken);

            foreach (var ev in packetDto.Events)
            {
                var pe = _mapper.Map<PersistEvent>(ev);

                var e = _eventConverter.ToMediatorEvent(pe);

                if (e == null)
                {
                    throw new DomainException($"Can't create mediator event for {ev}");
                }

                await _mediator.Send(e, cancellationToken);
            }

            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<(string FileName, byte[] Data)?> BuildOutboudZipAsync(Guid targetBranchId, CancellationToken cancellationToken)
        {
            var pkt = await BuildOutboudPacketAsync(targetBranchId, cancellationToken);

            if (pkt is null)
            {
                return null;
            }

            var jws = _replicationHelper.SerializeAndSign(pkt);

            var filename = _replicationHelper.ConstructFileName(pkt);

            var compressed = _replicationHelper.Compress(jws);

            return (filename, compressed);
        }

        public async Task<ReplictionPacketDto?> BuildOutboudPacketAsync(Guid targetBranchId, CancellationToken cancellationToken)
        {
            var currentBranchId = await _replicationRepository.GetCurretBranchAsync(cancellationToken);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var last = await _replicationRepository.GetLastAsync(targetBranchId, true, cancellationToken);

            var from = last?.ToDate;

            var now = DateTime.UtcNow;

            var events = _eventStoreRepository.GetEventsAsync(currentBranchId, from, now);

            var eventsDtos = new List<EventDto>();

            await foreach (var e in events)
            {
                var eDto = _mapper.Map<EventDto>(e);
                eventsDtos.Add(eDto);
            }

            if (!eventsDtos.Any())
            {
                return null;
            }

            var pkt = new ReplictionPacket()
            {
                BranchId = targetBranchId,
                Date = now,
                FromDate = from.HasValue ? from.Value : DateTime.MinValue,
                ToDate = now,
                Outbound = true,
                PreviousId = last?.Id,
            };

            await _replicationRepository.InsertAsync(pkt, cancellationToken);

            var pktDto = _mapper.Map<ReplictionPacketDto>(pkt);
            pktDto.SourceBranchId = currentBranchId;
            pktDto.Events = eventsDtos;

            await _unitOfWork.SaveAsync(cancellationToken);

            return pktDto;
        }

        public async Task<PagedResult<ReplictionPacketViewDto>> GetPagedHistoryAsync(ReplicationQueryDto querydto, CancellationToken cancellationToken)
        {
            var result = new PagedResult<ReplictionPacketViewDto>();
            result.CurrentPage = querydto.Page ?? 1;
            result.PageSize = querydto.PageSize ?? 0;

            var fetched = await _replicationRepository.GetExtendedAsync(null, result.CurrentPage, result.PageSize, cancellationToken);

            result.RowCount = fetched.Total;

            var pageCount = (double)result.RowCount / result.PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            result.Results = fetched.Items
                .Select(e => _mapper.Map<ReplictionPacketViewDto>(e))
                .ToList();

            return result;
        }
    }
}