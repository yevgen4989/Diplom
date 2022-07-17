using AutoMapper;
using Entities.Bot;
using WebFramework.Api;

namespace BotCore.Sample.Models.Bot
{
    public class ApplicationTgRoleDto : BaseDto<ApplicationTgRoleDto, ApplicationTgRole, int>
    {
        public string Name { get; set; }
    }
    
    
    public class ApplicationTgRoleSelectDto : BaseDto<ApplicationTgRoleSelectDto, ApplicationTgRole, int>
    {
        public string Name { get; set; }
        
        public override void CustomMappings(IMappingExpression<ApplicationTgRole, ApplicationTgRoleSelectDto> mappingExpression)
        {
            mappingExpression
                .ForAllMembers(opts => 
                    opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}