using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Aggregator.Models;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {

        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public CheckOutModel(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }



        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {

            var username = "swn";
            Cart = await _basketService.GetBasket(username);

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {

            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return Page();
            }



            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;


            await _basketService.CheckoutBasket(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}