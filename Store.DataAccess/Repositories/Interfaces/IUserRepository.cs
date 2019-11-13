using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public interface IUserRepository
    {
        public void CreateRole(Roles role);
        public void DeleteRole(Roles role);
        public Task<IEnumerable<string>> GetUserRoles(Users user);
        public Task<bool> IsInRole(Users user, string role);
        public void AddToRole(Users user, string role);
        public void RemoveFromRole(Users user, string role);
        public Task<Users> FindById(int id);
        public Task<Users> FindByEmail(string email);
        public Task<Users> FindByName(string username);
        public void Create(Users user);
        public void Update(Users user);
        public void Remove(Users user);
        public void SignIn(Users user, bool isPersistent);
    }
}
