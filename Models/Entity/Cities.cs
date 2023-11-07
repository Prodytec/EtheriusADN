using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace EtheriusWebAPI.Models.Entity
{
    [Table("cities")]
    public class Cities
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
