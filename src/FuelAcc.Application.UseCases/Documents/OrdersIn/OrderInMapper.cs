﻿using AutoMapper;
using FuelAcc.Application.Dto.Documents;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents.OrdersIn
{
    public class OrderInMapper : Profile
    {
        public OrderInMapper()
        {
            CreateMap<OrderIn, OrderInDto>().ReverseMap();
        }
    }
}