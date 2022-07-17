using Newtonsoft.Json;

namespace WebFramework.FirebaseInfrastructure.Services
{
    using Microsoft.Extensions.Options;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.IO;

    using FirebaseInfrastructure.Models;   
    using FirebaseInfrastructure.Exceptions;

    public class FirebaseService : IFirebaseService
    {
        private readonly AppSettings _appSettings;

        private readonly IHttpClientFactory _httpClientFactory;

        public FirebaseService(IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory)
        {
            _appSettings = appSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FirebaseUserToken> SignInAnonymously()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "returnSecureToken", "true" }
            });

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = 
                await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_appSettings.WebAPIKey}", content);

            using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
            
            return await JsonSerializer.DeserializeAsync<FirebaseUserToken>(contentStream);
        }

        public async Task<FirebaseUserToken> SignInWithEmailAndPassword(string email, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", "true" }
            });

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_appSettings.WebAPIKey}", content);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
 
            return await JsonSerializer.DeserializeAsync<FirebaseUserToken>(contentStream);
        }

        public async Task<FirebaseUserToken> SignUpWithEmailAndPassword(string email, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", "true" }
            });

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_appSettings.WebAPIKey}", content);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
            
            return await JsonSerializer.DeserializeAsync<FirebaseUserToken>(contentStream);
        }

        public async Task<FirebaseOAuthUserToken> SignInWithGoogleAccessToken(string googleAccessToken)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "autoCreate", "true" },
                { "postBody", $"providerId=google.com&access_token={googleAccessToken}&nonce=nonce" },
                { "requestUri", "http://localhost" },
                { "returnIdpCredential", "true" },
                { "returnSecureToken", "true" }
            });

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={_appSettings.WebAPIKey}", content);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }

            return await JsonSerializer.DeserializeAsync<FirebaseOAuthUserToken>(contentStream);
        }

        public async Task<FirebaseRefreshToken> RefreshToken(string refreshToken)
        {

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            });

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync($"https://securetoken.googleapis.com/v1/token?key={_appSettings.WebAPIKey}", content);

            using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                var contentError = await JsonSerializer.DeserializeAsync<FirebaseContentError>(contentStream);

                throw new FirebaseException(contentError);
            }
            
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<object>(jsonString);
            
            FirebaseRefreshToken refresh = JsonConvert.DeserializeObject<FirebaseRefreshToken>(jsonString);
            
            return refresh;
        }
    }
}
