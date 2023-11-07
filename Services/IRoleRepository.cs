using EtheriusWebAPI.Models;

#pragma warning disable

namespace EtheriusWebAPI.Services
{
    public interface IRoleRepository
    {
        #region INTERFACES METHODS
        Task<Response> AddRole(RoleModel role);

        Task<Response> UpdateRole(RoleModel role);

        Task<Role> GetRoleById(int roleId);

        Task<Response> GetRoles();

        Task<Response> ChangeRoleStatus(int roleId, int status);
        #endregion
    }
}
