using System.Threading;
using System.Threading.Tasks;
using Application.DataAccess.Models;

namespace Application.DataAccess.Repository
{
    public interface IUserRepository
    {
        Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default);

        Task Save(User user);
    }
}