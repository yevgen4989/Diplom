namespace Entities.Bot
{
    public class UserFile : BaseEntity
    {
        public int Id { get; set; }

        public int TelegramUserId { get; set; }
        public ApplicationTgUser TelegramUser { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
    }
}