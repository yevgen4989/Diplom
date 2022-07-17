using System;
using AutoMapper;
using Entities;
using WebFramework.Api;

namespace BotCore.Sample.Models
{
    public class CurrencyDto : BaseDto<CurrencyDto, Currency, int>
    {
        public string Code { get; set; }
        public double Value { get; set; }
    }
    
    public class CurrencySelectDto : BaseDto<CurrencySelectDto, Currency, int>
    {
        public string Code { get; set; }
        public double Value { get; set; }

        public override void CustomMappings(IMappingExpression<Currency, CurrencySelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}