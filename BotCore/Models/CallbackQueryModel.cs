using Newtonsoft.Json;

namespace BotCore.Models
{
    public class CallbackQueryModel
    {
        [JsonProperty("p")]
        public string Path { get; set; }
    }
}
