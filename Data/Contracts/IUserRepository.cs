using System.Threading;
using System.Threading.Tasks;
using Entities;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<UserOption>
    {
        void Add(UserOption user);
        UserOption? GetByFirebaseId(string firebaseId);
        UserOption? GetByEmail(string email);
        bool IsExistByFirebaseId(string firebaseId);


        Task AddAsync(UserOption user, CancellationToken cancellationToken);
        Task<UserOption?> GetByFirebaseIdAsync(string firebaseId, CancellationToken cancellationToken);
        Task<UserOption?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> IsExistByFirebaseIdAsync(string firebaseId, CancellationToken cancellationToken);
    }
}