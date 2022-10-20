﻿using AutoMapper;
using EventBus.Messages.Events;
using ordering.application.Features.Commands.CheckoutOrder;


namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {

        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}