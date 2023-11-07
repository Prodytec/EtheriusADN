using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

#pragma warning disable

namespace EtheriusWebAPI.Models.Entity
{
    [Table("customers_categories")]
    public class Customer_Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// Gets or Sets name.
        /// </summary>
        [DataMember(Name = "name")]
        public string? name { get; set; }
    }
}
