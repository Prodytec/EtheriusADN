using EtheriusWebAPI.Models;
# pragma warning disable
namespace EtheriusWebAPI.Services
{
    public interface IMenuItemRepository
    {
        #region INTERFACES METHODS
        Task<Response> AddMenu(List<MenuItemModel> menu);

        Task<Response> GetAllMenuItems();
        #endregion
    }
}
