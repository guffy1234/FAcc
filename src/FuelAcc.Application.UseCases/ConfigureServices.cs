using FluentValidation;
using FuelAcc.Application.Dto.Accounting;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Application.Dto.Documents;
using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.DtoCommon.Paging;
using FuelAcc.Application.Interface;
using FuelAcc.Application.Interface.Accounting;
using FuelAcc.Application.Interface.Events;
using FuelAcc.Application.Interface.Replication;
using FuelAcc.Application.UseCases.Accounting;
using FuelAcc.Application.UseCases.Accounting.Handlers;
using FuelAcc.Application.UseCases.Authorization;
using FuelAcc.Application.UseCases.Commons.Behaviours;
using FuelAcc.Application.UseCases.Commons.Commands;
using FuelAcc.Application.UseCases.Commons.Commands.Handlers;
using FuelAcc.Application.UseCases.Commons.Events;
using FuelAcc.Application.UseCases.Commons.Events.Handlers;
using FuelAcc.Application.UseCases.Commons.Filtering;
using FuelAcc.Application.UseCases.Commons.Filtering.Handlers;
using FuelAcc.Application.UseCases.Commons.Queries;
using FuelAcc.Application.UseCases.Commons.Queries.Handlers;
using FuelAcc.Application.UseCases.Documents.OrdersIn;
using FuelAcc.Application.UseCases.Documents.OrdersMove;
using FuelAcc.Application.UseCases.Documents.OrdersOut;
using FuelAcc.Application.UseCases.Events;
using FuelAcc.Application.UseCases.Replication;
using FuelAcc.Application.UseCases.Reports;
using FuelAcc.Application.UseCases.Reports.Rests;
using FuelAcc.Application.UseCases.Reports.Transactions;
using FuelAcc.Domain.Commons;
using FuelAcc.Domain.Entities.Dictionaries;
using FuelAcc.Domain.Entities.Documents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FuelAcc.Application.UseCases
{
    public static class ConfigureServices
    {
        public static void AddInjectionApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // mediator interceptors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            // dictionaries
            services.AddDictionary<Branch, BranchDto, BranchQueryDto>();
            services.AddDictionary<Partner, PartnerDto, PartnerQueryDto>();
            services.AddDictionary<Storage, StorageDto, StorageQueryDto>();
            services.AddDictionary<Product, ProductDto, ProductQueryDto>();
            services.AddDictionary<Folder, FolderDto, FolderQueryDto>();
            services.AddDictionary<FileBlob, FileBlobDto, FileBlobQueryDto>();

            // documents
            services.AddDocument<OrderIn, OrderInDto, OrderInQueryDto>();
            services.AddDocument<OrderOut, OrderOutDto, OrderOutQueryDto>();
            services.AddDocument<OrderMove, OrderMoveDto, OrderMoveQueryDto>();
            // reports
            services.AddTransient(typeof(IRequestHandler<ReportTransactionsQuery, IAsyncEnumerable<ReportTransactionView>>),
               typeof(ReportTransactionsHandler));
            services.AddTransient(typeof(IRequestHandler<ReportRestsQuery, IAsyncEnumerable<ReportRestView>>),
              typeof(ReportRestsHandler));
            //accounting
            services.AddTransient(typeof(IRequestHandler<GetAvailableRestsQuery, IEnumerable<AvailableRestView>>),
              typeof(GetAvailableRestsHandler));

            // services
            services.AddScoped<ITransactionsService, TransactionsService>();
            services.AddScoped<IReplicationService, ReplicationService>();
            services.AddScoped<IReplicationHelper, ReplicationHelper>();
            services.AddScoped<IEventConverter, EventConverter>();
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<IExecutionContext, ExecutionContext>();

            services.AddTransient<IDocumentTransactionsProcessor<OrderMove>, OrderMoveTransactionsProcessor>();
            services.AddTransient<IDocumentTransactionsProcessor<OrderIn>, OrderInTransactionsProcessor>();
            services.AddTransient<IDocumentTransactionsProcessor<OrderOut>, OrderOutTransactionsProcessor>();
        }

        public static void AddDictionary<ENTITY, DTO, QUERY_DTO>(this IServiceCollection services)
            where DTO : class
            where QUERY_DTO : DictionaryQueryDto
            where ENTITY : class, IDictionaryEntity, new()
        {
            // Queries
            services.AddTransient(typeof(IRequestHandler<GetAllQuery<DTO>, IAsyncEnumerable<DTO>>),
                typeof(GetAllHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<GetByIdQuery<DTO>, DTO>),
                typeof(GetByIdHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));

            services.AddTransient(typeof(IRequestHandler<GetPagedByQueryDto<DTO, QUERY_DTO>, PagedResult<DTO>>),
                typeof(GetByQueryDtoHandler<ENTITY, DTO, QUERY_DTO, DictionaryAuthorizationPoint<ENTITY>>));
            // Filter
            services.AddTransient(typeof(IRequestHandler<QueryBuilderCommand<QUERY_DTO>, IEntityQueryBuilderBase>),
                typeof(DictionaryQueryHandler<ENTITY, QUERY_DTO>));

            // Commands
            services.AddTransient(typeof(IRequestHandler<CreateCommand<DTO>, Unit>),
                typeof(CreateCommandHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<DeleteCommand<DTO>, Unit>),
                typeof(DeleteCommandHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<UpdateCommand<DTO>, Unit>),
                typeof(UpdateCommandHandler<ENTITY, DTO, DictionaryAuthorizationPoint<ENTITY>>));
            // Events
            services.AddTransient(typeof(IRequestHandler<CreateEvent<ENTITY>, Unit>), typeof(CreateEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<DeleteEvent<ENTITY>, Unit>), typeof(DeleteEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<UpdateEvent<ENTITY>, Unit>), typeof(UpdateEventHandler<ENTITY>));
        }

        public static void AddDocument<ENTITY, DTO, QUERY_DTO>(this IServiceCollection services)
            where DTO : class
            where QUERY_DTO : DocumentQueryDto
            where ENTITY : class, IDocumentEntity, new()
        {
            // Queries
            services.AddTransient(typeof(IRequestHandler<GetAllQuery<DTO>, IAsyncEnumerable<DTO>>),
                typeof(GetAllHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<GetByIdQuery<DTO>, DTO>),
                typeof(GetByIdHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));

            services.AddTransient(typeof(IRequestHandler<GetPagedByQueryDto<DTO, QUERY_DTO>, PagedResult<DTO>>),
                typeof(GetByQueryDtoHandler<ENTITY, DTO, QUERY_DTO, DocumentAuthorizationPoint<ENTITY>>));
            // Filter
            services.AddTransient(typeof(IRequestHandler<QueryBuilderCommand<QUERY_DTO>, IEntityQueryBuilderBase>),
                typeof(DocumentQueryHandler<ENTITY, QUERY_DTO>));

            // Commands
            services.AddTransient(typeof(IRequestHandler<CreateCommand<DTO>, Unit>),
                typeof(CreateCommandHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<DeleteCommand<DTO>, Unit>),
                typeof(DeleteCommandHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            services.AddTransient(typeof(IRequestHandler<UpdateCommand<DTO>, Unit>),
                typeof(UpdateCommandHandler<ENTITY, DTO, DocumentAuthorizationPoint<ENTITY>>));
            // Events
            services.AddTransient(typeof(IRequestHandler<CreateEvent<ENTITY>, Unit>), typeof(CreateDocumentEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<DeleteEvent<ENTITY>, Unit>), typeof(DeleteDocumentEventHandler<ENTITY>));
            services.AddTransient(typeof(IRequestHandler<UpdateEvent<ENTITY>, Unit>), typeof(UpdateDocumentEventHandler<ENTITY>));
        }
    }
}