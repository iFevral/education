using System.Threading.Tasks;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get refresh token
        /// </summary>
        /// <returns>Refresh token</returns>
        public string GetRefreshToken(Users user, string fingerprint);

        /// <summary>
        /// Save refresh token
        /// </summary>
        public Task SaveRefreshToken(Users user, string fingerprint, string newToken);

        /// <summary>
        /// Remove refresh token
        /// </summary>
        public Task RemoveRefreshToken(Users user, string ipfingerprint);
    }
}
