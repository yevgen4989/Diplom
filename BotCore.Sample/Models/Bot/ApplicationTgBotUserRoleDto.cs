using AutoMapper;
using Entities.Bot;
using WebFramework.Api;

namespace BotCore.Sample.Models.Bot
{
    public class ApplicationTgBotUserRoleDto : BaseDto<ApplicationTgBotUserRoleDto, ApplicationTgBotUserRole>
    {
        public int TelegramBotUserId { get; set; }
        public int TelegramRoleId { get; set; }
    }

    public class ApplicationTgBotUserRoleSelectDto : BaseDto<ApplicationTgBotUserRoleSelectDto, ApplicationTgBotUserRole>
    {
        public int TelegramBotUserId { get; set; }
        public int TelegramRoleId { get; set; }
        
        public override void CustomMappings(IMappingExpression<ApplicationTgBotUserRole, ApplicationTgBotUserRoleSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}