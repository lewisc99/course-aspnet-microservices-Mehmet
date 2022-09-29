﻿using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;

using System.Threading.Tasks;

namespace Discount.Grpc.Services
{

    //works like a controller
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase //name we 
    {

        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            var coupon = await _repository.GetDiscount(request.ProductName);
           
            if (coupon == null)
            {
                //like not found.
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName}"));


            }

            _logger.LogInformation("Discount is retrived for productName: {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
             
            //using autoMapper to convert Object Coupon into CouponModel
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;

        }

    }
}
