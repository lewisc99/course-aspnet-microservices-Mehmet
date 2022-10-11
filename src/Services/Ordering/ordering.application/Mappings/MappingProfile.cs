using AutoMapper;
using ordering.application.Features.Commands.CheckoutOrder;
using ordering.application.Features.Queries.GetOrderList;
using ordering.domain.Entities;

namespace ordering.application.Mappings
{
    public class MappingProfile :Profile
    {

        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();

        }


    }
}
