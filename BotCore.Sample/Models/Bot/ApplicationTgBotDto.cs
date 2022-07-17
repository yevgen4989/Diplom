using AutoMapper;
using Entities.Bot;
using WebFramework.Api;

namespace BotCore.Sample.Models.Bot
{
    public class ApplicationTgBotDto : BaseDto<ApplicationTgBotDto, ApplicationTgBot, int>
    {
        public string Name { get; set; }
    }

    public class ApplicationTgBotSelectDto : BaseDto<ApplicationTgBotSelectDto, ApplicationTgBot, int>
    {
        public string Name { get; set; }
        
        public override void CustomMappings(IMappingExpression<ApplicationTgBot, ApplicationTgBotSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}