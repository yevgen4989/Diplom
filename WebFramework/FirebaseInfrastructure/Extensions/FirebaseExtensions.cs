﻿namespace WebFramework.FirebaseInfrastructure.Extensions
{  
    using Microsoft.Extensions.DependencyInjection;
    using FirebaseAdmin;
    using Google.Apis.Auth.OAuth2;

    public static class FirebaseExtensions
    {
        public static void AddFirebaseAdminWithCredentialFromFile(this IServiceCollection _, string path)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(path)
            });
        }
    }
}
