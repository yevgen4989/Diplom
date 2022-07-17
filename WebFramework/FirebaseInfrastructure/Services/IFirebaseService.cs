using WebFramework.FirebaseInfrastructure.Models;

namespace WebFramework.FirebaseInfrastructure.Services
{
    public interface IFirebaseService
    {
        Task<FirebaseUserToken> SignUpWithEmailAndPassword(string email, string password);
        
        
        Task<FirebaseUserToken> SignInAnonymously();
        Task<FirebaseUserToken> SignInWithEmailAndPassword(string email, string password);
        Task<FirebaseOAuthUserToken> SignInWithGoogleAccessToken(string googleIdToken);


        Task<FirebaseRefreshToken> RefreshToken(string refreshToken);
    }
}