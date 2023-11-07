using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

#pragma warning disable

namespace EtheriusWebAPI.Models.Entity
{

    [Table("master_roles")]
    public class Master_Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// Gets or Sets name.
        /// </summary>
        [DataMember(Name = "name")]
        public string? name { get; set; }

        /// <summary>
        /// Gets or Sets is_active.
        /// </summary>
        [DataMember(Name = "is_active")]
        public int is_active { get; set; }
    }
}
