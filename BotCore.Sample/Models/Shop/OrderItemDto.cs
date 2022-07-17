using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class OrderItemDto : BaseDto<OrderItemDto, OrderItem, int>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
    
    public class OrderItemSelectDto : BaseDto<OrderItemSelectDto, OrderItem, int>
    {
        public OrderSelectDto Order { get; set; }
        public ProductSelectDto Product { get; set; }
        
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public override void CustomMappings(IMappingExpression<OrderItem, OrderItemSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}