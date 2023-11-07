namespace EtheriusWebAPI.Models
{
    public class MenuItemModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? parent_id { get; set; }
    }
}
