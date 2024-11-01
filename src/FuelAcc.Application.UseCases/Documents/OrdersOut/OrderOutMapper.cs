﻿using AutoMapper;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents.OrdersOut
{
    public class OrderOutMapper : Profile
    {
        public OrderOutMapper()
        {
            CreateMap<OrderOut, OrderOutDto>().ReverseMap();
        }
    }
}