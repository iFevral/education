using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void CreateRole(string name);
        public void DeleteRole(string name);
        public Task<IEnumerable<string>> GetUserRoles(string id);
        public Task<bool> IsInRole(string id, string role);
        public void AddToRole(string id, string role);
        public void RemoveFromRole(string id, string role);
        public Task<Users> FindById(string id);
        public Task<Users> FindByEmail(string email);
        public Task<Users> FindByName(string username);
        public IEnumerable<Users> GetAll();
        public void Create(Users user);
        public void Update(Users user);
        public void Remove(Users user);
        public void SignIn(Users user, bool isPersistent);
    }
}
