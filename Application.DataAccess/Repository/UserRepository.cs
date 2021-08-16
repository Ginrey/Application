using System.Threading;
using System.Threading.Tasks;
using Application.DataAccess.Context;
using Application.DataAccess.Exceptions;
using Application.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext) =>
            this.dbContext = dbContext;

        public async Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default)=>
            await this.dbContext.Users.FirstOrDefaultAsync(p => p.Login == login, cancellationToken);

        public async Task Save(User user)
        {
            if (this.dbContext.Entry(user).State == EntityState.Detached)
                await this.dbContext.AddAsync(user);

            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                this.dbContext.ChangeTracker.Clear();

                throw new OptimisticConcurrencyException(e.Message, e);
            }
        }
    }
}