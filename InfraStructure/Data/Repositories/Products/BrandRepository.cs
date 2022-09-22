

using AutoMapper;
using Core.Dtos;
using Core.Entities.Products;
using Core.Interfaces;

namespace InfraStructure.Data.Repositories.Products
{
    public class BrandRepository: BaseRepository<Brand, BrandOutDto>, IBrandRepository
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public BrandRepository(StoreContext context, IMapper mapper): base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
