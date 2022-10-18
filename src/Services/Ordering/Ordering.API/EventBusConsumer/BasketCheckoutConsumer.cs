using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ordering.application.Features.Commands.CheckoutOrder;
using System;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }



        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {

            //context.Message means the Object.
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message); //wiill convert BasketCheckoutEvent to CheckoutOrderCommand.

            var result = await _mediator.Send(command); //will trigger the method handle from CheckoutOrderCommandHandler.cs (Ordering.application)

            _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id : {result} ");


        }
    }
}
