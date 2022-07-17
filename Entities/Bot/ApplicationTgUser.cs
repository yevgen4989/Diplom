using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BotCore.Data.Models;

namespace Entities.Bot
{
    public class ApplicationTgUser : TelegramUser<int>, IEntity<int>
    {
        [AllowNull]
        public override string? FirstName { get; set; }
        
        [AllowNull]
        public override string? LastName { get; set; }
        
        public ICollection<UserFile> UserFiles { get; set; }
    }
    
}