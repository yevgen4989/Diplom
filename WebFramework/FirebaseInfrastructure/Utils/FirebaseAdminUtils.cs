namespace WebFramework.FirebaseInfrastructure.Utils
{
    using FirebaseAdmin.Auth;
    using System.Threading.Tasks;

    public interface IFirebaseAdminUtils
    {
        public Task<FirebaseToken> ValidateToken(string token);
    }

    public class FirebaseAdminUtils : IFirebaseAdminUtils
    {
        public async Task<FirebaseToken> ValidateToken(string token)
        {
            if (token == null)
                return null;

            
            try
            {
                return await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            }
            catch
            {
                return null;
            }
        }
    }
}
