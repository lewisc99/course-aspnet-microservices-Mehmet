
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{


    [ApiController]
    [Route("api/v1/[Controller]")]
    public class ShoppingController : ControllerBase
    {


        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;


        public ShoppingController(ICatalogService _catalogService, IBasketService _basketService, IOrderService _orderService)
        {


            this._catalogService = _catalogService ?? throw new ArgumentNullException(nameof(_catalogService));
            this._basketService = _basketService ?? throw new ArgumentNullException(nameof(_basketService));
            this._orderService = _orderService ?? throw new ArgumentNullException(nameof(_orderService));

        }



        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel),(int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {

            //get basket with username
            // interate basket items and consume products with basket item productId member
            //map product related members into basketitem dto with extends columns
            //consume ordering microservices in order to retrieve order list
            //return root ShoppignModel dto class which including all responses

            var basket = await _basketService.GetBasket(userName);

            foreach(var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);

                //set additional product fields onto basket item.
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;

            }

            var orders = await _orderService.GetOrdersByUserName(userName);


            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                basketWithProducts = basket,
                Orders = orders

            };

            return Ok(shoppingModel);

        }



    }
}
