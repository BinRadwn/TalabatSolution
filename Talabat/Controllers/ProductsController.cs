using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.specification;
using Talabat.Dtos;
using Talabat.Error;
using Talabat.Helpers;

namespace Talabat.Controllers
{

    public class ProductsController : BaseApiController
    {
        //Property
        private readonly IGenaricRepository<Product> _productRepo;
        private readonly IGenaricRepository<ProductBrand> _brandsRepo;
        private readonly IGenaricRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        //Constructor
        public ProductsController(IGenaricRepository<Product> ProductRepo,
            IGenaricRepository<ProductBrand> BrandsRepo,
            IGenaricRepository<ProductType> TypesRepo,
             IMapper mapper)
        {
            _productRepo = ProductRepo;
            _brandsRepo = BrandsRepo;
            _typesRepo = TypesRepo;
            _mapper = mapper;
        }

        //End Points

        [CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductspecParam productParam)
        {
            var spec = new ProductWithBrandAndTypeSpecification(productParam);

            var Products = await _productRepo.GetAllWithSpecAsync(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var CountSpec = new ProductWithFilterForCountSpecification(productParam);
            var count = await _productRepo.GetCountAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDto>(productParam.PageIndex, productParam.PageSize,count ,Data ));
        }

        [CachedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);

            var Product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (Product == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDto>(Product));
        }


        [CachedAttribute(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }


        [CachedAttribute(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
             
        }

    }
}
