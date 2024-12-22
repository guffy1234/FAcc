using FuelAcc.Application.Dto.Querying;
using FuelAcc.Application.DtoCommon.Dictionaries;
using FuelAcc.Domain.Commons;

namespace FuelAcc.Application.UseCases.Commons.Filtering
{
    public class DictionaryQueryBuilder<ENTITY, QUERY_DTO> : IDictionaryQueryBuilder<ENTITY>
       where ENTITY : class, IDictionaryEntity
       where QUERY_DTO : DictionaryQueryDto
    {
        public DictionaryQueryBuilder(QUERY_DTO dto)
        {
            Dto = dto;
        }

        public QUERY_DTO Dto { get; }

        public int? Page => Dto.Page;

        public int? PageSize => Dto.PageSize;

        public IQueryable<ENTITY> Sort(IQueryable<ENTITY> query)
        {
            return query.OrderBy(d => d.Name);
        }

        IQueryable<ENTITY> IEntityQueryBuilder<ENTITY>.Filter(IQueryable<ENTITY> query)
        {
            if (!string.IsNullOrEmpty(Dto.Name))
            {
                query = query.Where(d => d.Name.Contains(Dto.Name));
            }
            if (typeof(ENTITY).IsAssignableTo(typeof(IDictionaryWithFolderEntity)) &&
                Dto is IDictionaryWithFoldersQueryDto folders &&
                folders.FolderId?.Any() == true)
            {
                if (folders.FolderId.Count() > 1)
                {
                    query = query.Where(d => folders.FolderId.Contains((d as IDictionaryWithFolderEntity).FolderId.Value));
                }
                else
                {
                    query = query.Where(d => folders.FolderId.First() == (d as IDictionaryWithFolderEntity).FolderId.Value);
                }
            }
            return query;
        }
    }
}