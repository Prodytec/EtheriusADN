#pragma warning disable
namespace EtheriusWebAPI.Models
{  
    public class RoleModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public List<int> menu_ids { get; set; }
    } 
    public class MenuTreeViewModel
    {
        public int Key { get; set; }
        public string Title { get; set; }
        public bool isLeaf { get; set; }
        public List<MenuTreeViewModel> children { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<MenuTreeViewModel> menuItems { get; set; }
    }
}
