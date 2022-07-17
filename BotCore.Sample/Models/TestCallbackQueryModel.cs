using BotCore.Models;
using Newtonsoft.Json;

namespace BotCore.Sample.Models
{
    public class TestCallbackQueryModel : CallbackQueryModel
    {
        [JsonProperty("a")]
        public bool SomeData { get; set; }
    }
}