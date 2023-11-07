using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
#pragma warning disable

namespace EtheriusWebAPI.Services
{
    public class RoleRepository : IRoleRepository
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
        public RoleRepository(ApplicationDbContext _db)
        {
            db = _db;
            response = new Models.Response();
        }

        /// <summary>
        /// add a new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Response> AddRole(RoleModel role)
        {
            if (db != null)
            {
                try
                {
                    var existingRole = await db.Master_Roles.FirstOrDefaultAsync(x => x.name == role.name);
                    if (existingRole == null)
                    {
                        var newRole = new Master_Roles
                        {
                            name = role.name,
                            is_active = 1,
                        };
                        await db.Master_Roles.AddAsync(newRole);
                        await db.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Message = "New Role Created Successfully";
                        response.Data = newRole;

                        if (role.menu_ids.Any())
                        {
                            foreach (var menu_item_id in role.menu_ids)
                            {
                                var menu_item = await db.Master_Menu_Items.FindAsync(menu_item_id);
                                db.User_Permissions.Add(new User_Permissions()
                                {
                                    role_id = newRole.id,
                                    menu_item_id = menu_item.id
                                });
                            }
                           await db.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        if (role.menu_ids.Any())
                        {
                            foreach (var menu_item_id in role.menu_ids)
                            {
                                var menu_item = await db.Master_Menu_Items.FindAsync(menu_item_id);
                                db.User_Permissions.Add(new User_Permissions()
                                {
                                    role_id = existingRole.id,
                                    menu_item_id = menu_item.id
                                });
                            }
                            await db.SaveChangesAsync();
                        }
                        response.IsSuccess = true;
                        response.Message = "Existing Role Updated.";
                        response.Data = existingRole;
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                }
            }
            return response;
        }


        /// <summary>
        /// update a role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Response> UpdateRole(RoleModel role)
        {
            if (db != null)
            {
                try
                {
                    var existingRole = await db.Master_Roles.FindAsync(role.id);
                    if (existingRole != null)
                    {
                        // delete all records from user permission
                        db.User_Permissions.RemoveRange(db.User_Permissions.Where(x => x.role_id == role.id));
                        await db.SaveChangesAsync();

                        var newRole = new Master_Roles
                        {
                            name = role.name,
                        };
                        newRole.id = existingRole.id;

                        await db.SaveChangesAsync();

                        if (role.menu_ids.Any())
                        {
                            foreach (var menu_item_id in role.menu_ids)
                            {
                                var menu_item = await db.Master_Menu_Items.FindAsync(menu_item_id);
                                db.User_Permissions.Add(new User_Permissions()
                                {
                                    role_id = newRole.id,
                                    menu_item_id = menu_item.id
                                });
                            }
                           await db.SaveChangesAsync();
                        }
                        response.IsSuccess = true;
                        response.Message = "Role Updated Successfully.  ";
                        response.Data = existingRole;
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                }
            }
            return response;
        }


        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleById(int roleId)
        {
            if (db != null)
            {
                try
                {
                    // get role by id
                    var role = await db.Master_Roles.Where(a => a.id == roleId).FirstOrDefaultAsync();

                    // get permissions assigned
                    var roleMenus = await (from mr in db.Master_Roles.Where(role => role.is_active == 1)

                                           join jup in db.User_Permissions on mr.id equals jup.role_id into gup
                                           from up in gup.DefaultIfEmpty()

                                           join jmm in db.Master_Menu_Items on up.menu_item_id equals jmm.id into gmm
                                           from mm in gmm.DefaultIfEmpty()
                                           where mr.id == roleId
                                           select mm).ToListAsync();
                    if (roleMenus.Count > 0 && roleMenus[0] != null)
                    {
                        // get all menus
                        var menuItems = await (from mm in db.Master_Menu_Items select mm).ToListAsync();

                        var roots = roleMenus.Where(menu => menu.parent_menu_item_id == null);
                        var roleMenuTree = new List<MenuTreeViewModel>();

                        foreach (var root in roots)
                        {
                            var treeItem = BuildRoleMenuTree(root, menuItems);
                            roleMenuTree.Add(treeItem);
                        }

                        var roleInfo = new Role
                        {
                            RoleId = role.id,
                            RoleName = role.name,
                            menuItems = roleMenuTree
                        };

                        return roleInfo;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }


        /// <summary>
        /// Get All roles 
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetRoles()
        {
            try
            {
                if (db != null)
                {
                    var customers = await db.Master_Roles.ToListAsync();
                    if (customers.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Roles Fetched Successfully";
                        response.Data = customers;
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "No Roles Found";
                        response.Data = customers;
                    }
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
        /// tree view for menu 
        /// </summary>
        /// <param name="currentMenu"></param>
        /// <param name="allMenus"></param>
        /// <returns></returns>
        private MenuTreeViewModel BuildRoleMenuTree(Master_Menu_Items currentMenu, List<Master_Menu_Items> allMenus)
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


        /// <summary>
        /// Activate or deactivate a role - 1 active , 0 deactive
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<Response> ChangeRoleStatus(int roleId, int status)
        {
            if (db != null)
            {
                try
                {
                    // Find the role by ID
                    var role = await db.Master_Roles.FindAsync(roleId);

                    if (role != null)
                    {
                        // Update the is_active property based on the 'activate' parameter
                        role.is_active = status;

                        // Save the changes to the database
                        await db.SaveChangesAsync();

                        response.IsSuccess = true;
                        response.Message = "Status Changed Successfully.";
                        response.Data = role;
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Null Exception";
                response.Data = "DbContext has null Value";
            }
            return response;
        }
    }
}
