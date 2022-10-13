using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ordering.application.Features.Queries.GetOrderList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrdersVm>>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;


        public  GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
           _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public  async Task<List<OrdersVm>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {


           var orderList =  await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orderList);


        }
    }
}
