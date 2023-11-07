using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

#pragma warning disable

namespace EtheriusWebAPI.Services
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Class Variable Declaration
        /// </summary>
        private readonly ApplicationDbContext db;
        private readonly Models.Response response;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="_db"></param>
        public UserRepository(ApplicationDbContext _db)
        {
            db = _db;
            response = new Models.Response();
        }


        /// <summary>
        /// add a new user , also adding roles for a user
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Models.Response> AddUser(UserModel user)
        {
            try
            {
                if (db != null)
                {
                    var existingUser = await db.Users.Where(a => a.email == user.email).FirstOrDefaultAsync();
                    var newUser = new Users();
                    if (existingUser == null)
                    {
                        newUser = new Users
                        {
                            first_name = user.first_name,
                            last_name = user.last_name,
                            email = user.email,
                            password = user.password,
                            is_active = 1,
                            created_date = DateTime.Now,
                        };
                        await db.AddAsync(newUser);
                        await db.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Message = "New User Created Successfully";
                        response.Data = newUser;
                    }

                    // Create associations with user_roles
                    foreach (var item in user.roles)
                    {
                        var User_Roles = new User_Roles();
                        var role = await db.Master_Roles.FindAsync(item.id);
                        if (role != null)
                        {
                            if (newUser != null)
                            {
                                User_Roles = (new User_Roles()
                                {
                                    user_id = newUser.id,
                                    role_id = role.id,
                                });
                            }
                            else
                            {
                                User_Roles = (new User_Roles()
                                {
                                    user_id = existingUser.id,
                                    role_id = role.id,
                                });
                            }
                            await db.User_Roles.AddAsync(User_Roles);
                            await db.SaveChangesAsync();
                        }
                    }
                    await db.SaveChangesAsync();
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
        /// update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Models.Response> UpdateUser(UserModel user)
        {
            try
            {
                if (db != null)
                {
                    var existingUser = await db.Users.FindAsync(user.id);
                    if (existingUser != null)
                    {
                        var newUser = new Users
                        {
                            first_name = user.first_name,
                            last_name = user.last_name,
                            email = user.email,
                            password = user.password,
                            is_active = 1,
                            created_date = DateTime.Now,
                        };
                        db.Update(newUser);
                        await db.SaveChangesAsync();

                        // Create associations with user_roles
                        foreach (var item in user.roles)
                        {
                            var User_Roles = new User_Roles();
                            var role = await db.Master_Roles.FindAsync(item.id);
                            if (role != null)
                            {
                                User_Roles = (new User_Roles()
                                {
                                    user_id = newUser.id,
                                    role_id = role.id,
                                });

                                db.User_Roles.Update(User_Roles);
                                await db.SaveChangesAsync();
                            }
                        }

                        if (user != null)
                        {
                            response.IsSuccess = true;
                            response.Message = "Updated User Successfully";
                            response.Data = newUser;
                        }
                    }
                    await db.SaveChangesAsync();
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
        /// GetUserWithRolesAndPermissions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserWithRolesAndPermissions> GetUserWithRolesAndPermissions(string email, string password)
        {
            try
            {
                var user = await (from u in db.Users
                                  where u.email == email && u.password == password
                                  select new UserWithRolesAndPermissions
                                  {
                                      id = u.id,
                                      first_name = u.first_name,
                                      last_name = u.last_name,
                                      email = u.email,
                                      password = u.password,
                                  }).FirstOrDefaultAsync();

                if (user != null)
                {
                    var roles = await (from ur in db.User_Roles
                                 join mr in db.Master_Roles on ur.role_id equals mr.id
                                 where ur.user_id == user.id
                                 select new UserRoles
                                 {
                                     id = mr.id,
                                     name = mr.name,
                                 }).ToListAsync();

                    foreach (var data in roles)
                    {
                        var menuItems = await (from mm in db.Master_Menu_Items select mm).ToListAsync();
                        var roleMenus = await (from mr in db.Master_Roles
                                               join jup in db.User_Permissions on mr.id equals jup.role_id into gup
                                               from up in gup.DefaultIfEmpty()
                                               join jmm in db.Master_Menu_Items on up.menu_item_id equals jmm.id into gmm
                                               from mm in gmm.DefaultIfEmpty()
                                               where mr.id == data.id
                                               select mm).ToListAsync();

                        var roots = roleMenus.Where(menu => menu.parent_menu_item_id == null);
                        var roleMenuTree = new List<MenuTreeViewModel>();

                        foreach (var root in roots)
                        {
                            var treeItem = BuildRoleMenuTree(root, menuItems);
                            roleMenuTree.Add(treeItem);
                        }

                        // Assign the roleMenuTree to the UserRoles
                        data.roleMenuTree = roleMenuTree;
                    }

                    user.Roles = roles;
                    response.IsSuccess = true;
                    response.Message = "User Found Successfully";
                    response.Data = user;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Wrong email or password";
                    response.Data = user;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// BuildRoleMenuTree
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
    }
}
