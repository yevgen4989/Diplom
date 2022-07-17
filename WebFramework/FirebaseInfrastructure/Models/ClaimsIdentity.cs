using Newtonsoft.Json;

namespace WebFramework.FirebaseInfrastructure.Models
{
    public class ClaimsIdentity {
        [JsonProperty("identities")]
        public Identities Identities { get; set; }
        
        [JsonProperty("sign_in_provider")]
        public string signInProvider { get; set; }
    }
}