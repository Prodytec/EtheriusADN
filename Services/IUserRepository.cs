using EtheriusWebAPI.Models;
# pragma warning disable
namespace EtheriusWebAPI.Services
{
    public interface IUserRepository
    {
        #region INTERFACES METHODS
        Task<Response> AddUser(UserModel user);
        Task<Response> UpdateUser(UserModel user);
        Task<UserWithRolesAndPermissions> GetUserWithRolesAndPermissions(string email, string password);
        #endregion
    }
}
