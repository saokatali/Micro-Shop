using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ordering.API.Application.Dtos;
using Ordering.API.Domain.Models.Entities;

namespace Ordering.API.Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

        }
    }
}
