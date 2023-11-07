using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data;

#pragma warning disable

namespace EtheriusWebAPI.Services
{
    public class MenuItemRepository : IMenuItemRepository
    {
        /// <summary>
        /// Class Variable Declaration
        /// </summary>
        private readonly ApplicationDbContext db;
        private readonly Models.Response response;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_db"></param>
        public MenuItemRepository(ApplicationDbContext _db)
        {
            db = _db;
            response = new Models.Response();
        }

        /// <summary>
        /// add a new menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<Response> AddMenu(List<MenuItemModel> menu)
        {
            var menuIds = new List<int>();
            try
            {

                if (db != null)
                {
                    foreach (var menuItem in menu)
                    {
                        if (menuItem.parent_id == 0)
                            menuItem.parent_id = null;
                        var newMenu = new Master_Menu_Items
                        {
                            menu_item_name = menuItem.name,
                            parent_menu_item_id = menuItem.parent_id,

                        };

                        await db.Master_Menu_Items.AddAsync(newMenu);
                        await db.SaveChangesAsync();

                        menuIds.Add(newMenu.id);
                    }
                    response.IsSuccess = true;
                    response.Message = "Menu Added Successfully";
                    response.Data = menu;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// get all menu items in a tree view
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetAllMenuItems()
        {
            try
            {
                var menuItems = await (from mm in db.Master_Menu_Items select mm).ToListAsync();
                var roots = menuItems.Where(menu => menu.parent_menu_item_id == null);
                var menuTree = new List<MenuTreeViewModel>();

                foreach (var root in roots)
                {
                    var treeItem = BuildRoleMenuTree(root, menuItems);
                    menuTree.Add(treeItem);
                }
                if (menuTree.Count > 0)
                {
                    response.IsSuccess = true;
                    response.Message = "MenuItem Fetched Successfully";
                    response.Data = menuTree;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "No MenuItem Found";
                    response.Data = menuTree;

                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// tree view for menu
        /// </summary>
        /// <param name="currentMenu"></param>
        /// <param name="allMenus"></param>
        /// <returns></returns>
        public MenuTreeViewModel BuildRoleMenuTree(Master_Menu_Items currentMenu, List<Master_Menu_Items> allMenus)
        {
            var treeItem = new MenuTreeViewModel
            {
                Key = currentMenu.id,
                Title = currentMenu.menu_item_name,
                isLeaf = !allMenus.Any(menu => menu.parent_menu_item_id == currentMenu.id)
            };

            var childMenus = allMenus.Where(menu => menu.parent_menu_item_id == currentMenu.id).ToList();
            treeItem.children = childMenus.Select(childMenu => BuildRoleMenuTree(childMenu, allMenus)).ToList();

            return treeItem;
        }
    }
}
