using Core.Dtos;
using Core.Entities.Products;
using Core.Interfaces;
using InfraStructure.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Products
{
    public class BrandController: BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public BrandController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<List<BrandOutDto>> GetAllBrands()
        {
            return await _uow.BrandRepository.GetAllAsync();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BrandOutDto>> AddBrand(BrandInDto brandParams)
        {
            var newBrand = new Brand { Name = brandParams.Name };
            var addedBrand = _uow.BrandRepository.Add(newBrand);

            if (await _uow.Complete()) return Ok(_uow.BrandRepository.MapToDto(addedBrand));
            return BadRequest(new ApiException(400));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BrandOutDto>> UpdateBrand(BrandUpdateDto brandParams)
        {
            var updatedBrand = _uow.BrandRepository.Update(new Brand { Id = brandParams.Id, Name = brandParams.Name });
            if (updatedBrand is null) return BadRequest(new ApiException(400, "Item Not Found"));

            if (await _uow.Complete()) return Ok(_uow.BrandRepository.MapToDto(updatedBrand));
            return BadRequest(new ApiException(400));
        }
    }
}
