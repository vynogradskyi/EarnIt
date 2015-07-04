using EarnIt.Ninja.Services.Domain.Models;

namespace EarnIt.Ninja.Services.Contract.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Add(User user);
        User Get(string login);
        void Update(User user);
    }
}