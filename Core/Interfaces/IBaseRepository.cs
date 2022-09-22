
using Core.Dtos;
using Core.Entities;
using Core.Helpers;

namespace Core.Interfaces
{
    public interface IBaseRepository<baseEntity, outDto>
        where baseEntity : BaseEntity
        where outDto : OutDto
    {
        Task<Pagination<outDto>> GetListAsync(ISpecification<baseEntity> specification, ISpecification<baseEntity> countspecification, PaginationParams paginationParams);
        Task<outDto> GetByIdAsync(ISpecification<baseEntity> spec);
        Task<List<outDto>> GetAllAsync();
        outDto MapToDto(baseEntity entity);
        Task<int> CountAsync(ISpecification<baseEntity> countspecification);
        baseEntity Add(baseEntity entity);
        void Delete(baseEntity entity);
        baseEntity Update(baseEntity entity, string?[] noModifyProperties = null);
    }
}
