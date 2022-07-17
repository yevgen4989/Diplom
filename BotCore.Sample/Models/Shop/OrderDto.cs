using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class OrderDto : BaseDto<OrderDto, Order, int>
    {
        public int AccountNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ClientName { get; set; }
        public int PaymentId { get; set; }
        public int DeliveryId { get; set; }
        public int UserOptionId { get; set; }
    }
    
    public class OrderSelectDto : BaseDto<OrderSelectDto, Order, int>
    {
        public int AccountNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ClientName { get; set; }
        public PaymentSelectDto Payment { get; set; }
        public DeliverySelectDto Delivery { get; set; }
        public int UserOptionId { get; set; }

        public override void CustomMappings(IMappingExpression<Order, OrderSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}