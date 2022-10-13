using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ordering.application.Features.Commands.CheckoutOrder;
using ordering.application.Features.Commands.DeleteOrder;
using ordering.application.Features.Commands.UpdateOrder;
using ordering.application.Features.Queries.GetOrderList;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>),(int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrderByUserName(string userName)
        {
            var query = new GetOrderListQuery(userName);
            var orders = await _mediator.Send(query);

            return Ok(orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPut(Name ="UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}",Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand() { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }


    }
}
