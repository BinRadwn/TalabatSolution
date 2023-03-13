using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Dtos;

namespace Talabat.Controllers
{

    public class BasketController : BaseApiController
    {
        //
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;


        //Constructor
        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }



        //End Point

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var Mapped = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var UpdatedOrCreated = await _basketRepository.UpdateBasketAsync(Mapped);
            return Ok(UpdatedOrCreated);

        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
