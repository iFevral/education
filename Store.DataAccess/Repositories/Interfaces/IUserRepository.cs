using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task CreateRole(string name);
        public Task DeleteRole(string name);
        public Task<IEnumerable<string>> GetUserRoles(string id);
        public Task<bool> IsInRole(string id, string role);
        public Task AddToRole(string id, string role);
        public Task RemoveFromRole(string id, string role);
        public Task<Users> FindById(string id);
        public Task<Users> FindByEmail(string email);
        public Task<Users> FindByName(string username);
        public IEnumerable<Users> GetAll();
        public Task Create(Users user, string password);
        public Task Update(Users user);
        public Task Remove(Users user);
        public Task<bool> IsCreated(string username, string password);
    }
}
