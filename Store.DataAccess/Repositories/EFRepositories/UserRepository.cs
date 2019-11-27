using System.Linq;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }

        public string GetRefreshToken(Users user, string ipfingerprint)
        {
            var session = _db.Sessions.Where(s => s.UserId.Equals(user.Id) &&
                                            s.IPFingerprint.Equals(ipfingerprint));

            return session.Any()
                ? session.FirstOrDefault().RefreshToken
                : null;
        }

        public async Task SaveRefreshToken(Users user, string ipfingerprint, string newToken)
        {
            _db.Sessions.Add(new Sessions 
            { 
                UserId = user.Id,
                IPFingerprint = ipfingerprint,
                RefreshToken = newToken
            });
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRefreshToken(Users user, string ipfingerprint)
        {
            var session = _db.Sessions.Where(s => s.UserId.Equals(user.Id) &&
                                                  s.IPFingerprint.Equals(ipfingerprint)).FirstOrDefault();
            _db.Sessions.Remove(session);
            await _db.SaveChangesAsync();
        }
    }
}
