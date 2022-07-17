using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class PaymentDto : BaseDto<PaymentDto, Payment, int>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public TypePayment TypePayment { get; set; }
    }
    
    public class PaymentSelectDto : BaseDto<PaymentSelectDto, Payment, int>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public TypePayment TypePayment { get; set; }

        public override void CustomMappings(IMappingExpression<Payment, PaymentSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}