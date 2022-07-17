using Newtonsoft.Json;

namespace WebFramework.FirebaseInfrastructure.Models
{
    
    //"access_token": "[ACCESS_TOKEN]",
    //"expires_in": "3600",
    //"token_type": "Bearer",
    // "refresh_token": "[REFRESH_TOKEN]",
    // "id_token": "[ID_TOKEN]",
    // "user_id": "nK5zy6unFEgOaXBKYy1GIH0KYyn1",
    // "project_id": "481784122717"
    
    
    
    public class FirebaseRefreshToken
    {
        [JsonProperty("access_token")]
        public string accessToken { get; set; }
        
        [JsonProperty("expires_in")]
        public string expiresIn { get; set; }
        
        [JsonProperty("token_type")]
        public string tokenType { get; set; }
        
        [JsonProperty("refresh_token")]
        public string refreshToken { get; set; }
        
        [JsonProperty("id_token")]
        public string idToken { get; set; }
        
        [JsonProperty("user_id")]
        public string userId { get; set; }
        
        [JsonProperty("project_id")]
        public string projectId { get; set; }
    }
}