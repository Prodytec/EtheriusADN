using System.Data;
#pragma warning disable
namespace EtheriusWebAPI.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public List<Roles> roles { get; set; }
    }

    public class UserWithRoles
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public List<UserRoles> Roles { get; set; }
    }

    public class UserWithRolesAndPermissions
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public List<UserRoles> Roles { get; set; }
    }

    public class UserRoles
    {
        public int id { get; set; }
        public string? name { get; set; }
        public List<MenuTreeViewModel> roleMenuTree { get; set; }
    }

    public class Roles
    {
        public int id { get; set; }
        //public List<int> menu_id { get; set; }
    }
}
