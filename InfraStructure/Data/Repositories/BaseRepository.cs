
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using InfraStructure.Errors;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data.Repositories
{
    public class BaseRepository<T, outDto> : IBaseRepository<T, outDto>
        where T: BaseEntity
        where outDto: OutDto
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public BaseRepository(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public async Task<int> CountAsync(ISpecification<T> countspecification)
        {
            return await ApplySpecification(countspecification).CountAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<outDto>> GetAllAsync()
        {
            return await _context.Set<T>().ProjectTo<outDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
        }

        public async Task<outDto> GetByIdAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ProjectTo<outDto>(_mapper.ConfigurationProvider).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Pagination<outDto>> GetListAsync(ISpecification<T> specification, ISpecification<T> countspecification, PaginationParams paginationParams)
        {
            var list = await ApplySpecification(specification).ProjectTo<outDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
            return new Pagination<outDto>(
                list,
                paginationParams.PageNumber,
                paginationParams.PageSize,
                CountAsync(countspecification).Result
            );
        }

        public outDto MapToDto(T entity)
        {
            return _mapper.Map<outDto>(entity);
        }

        public T Update(T entity, string?[] noModifyProperties = null)
        {
            var entFound = _context.Set<T>().AsNoTracking().FirstOrDefault(s => s.Id == entity.Id);
            if (entFound == null) return null;

            var attEnt = _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            if (noModifyProperties != null && noModifyProperties.Length > 0)
            {
                foreach (var property in noModifyProperties)
                {
                    _context.Entry(entity).Property(property).IsModified = false;
                }
            }
            return attEnt.Entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.ApplySpecification(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
