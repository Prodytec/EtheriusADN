using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
# pragma warning disable
namespace EtheriusWebAPI.Models.Entity
{
    [Table("states")]
    public class States
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
