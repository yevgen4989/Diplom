using Common;
using Common.Exceptions;
using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<UserOption>, IUserRepository, IScopedDependency
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }


        public void Add(UserOption user) {
            var exists = TableNoTracking.Any(p => p.FirebaseId == user.FirebaseId);
            if (exists) {
                throw new BadRequestException("Пользователь с ID: "+user.FirebaseId+" уже существует");   
            }

            base.Add(user);
        }
        public UserOption? GetByFirebaseId(string firebaseId) {
            UserOption? userOption = Table.SingleOrDefault(p => p.FirebaseId == firebaseId);

            return userOption;
        }

        public UserOption? GetByEmail(string email) {
            UserOption? userOption = Table.SingleOrDefault(p => p.Email == email);

            return userOption;
        }

        public bool IsExistByFirebaseId(string firebaseId)
        {
            UserOption? userOption = Table.SingleOrDefault(p => p.FirebaseId == firebaseId);

            return userOption != null;
        }


        
        
        public async Task AddAsync(UserOption user, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.FirebaseId == user.FirebaseId);
            if (exists) {
                throw new BadRequestException("Пользователь с ID: "+user.FirebaseId+" уже существует");   
            }

            await base.AddAsync(user, cancellationToken);
        }

        public async Task<UserOption?> GetByFirebaseIdAsync(string firebaseId, CancellationToken cancellationToken)
        {
            UserOption? userOption = await Table.Where(p => p.FirebaseId == firebaseId).SingleOrDefaultAsync(cancellationToken);

            return userOption;
        }

        public async Task<UserOption?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            UserOption? userOption = await Table.Where(p => p.Email == email).SingleOrDefaultAsync(cancellationToken);

            return userOption;
        }
        
        public async Task<bool> IsExistByFirebaseIdAsync(string firebaseId, CancellationToken cancellationToken)
        {
            UserOption? userOption = await Table.Where(p => p.FirebaseId == firebaseId).SingleOrDefaultAsync(cancellationToken);

            return userOption != null;
        }
        
    }
}
