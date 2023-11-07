namespace EtheriusWebAPI.Models
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set;}
        public string? email { get; set; }
        public string? password { get; set; }
        public int? city { get; set; }
        public int? category { get; set; }
        public int? state { get; set; }
        public int? tax_condition { get; set; }
    }
}
