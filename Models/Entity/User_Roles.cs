using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

#pragma warning disable

namespace EtheriusWebAPI.Models.Entity
{
    [Table("user_roles")]
    public class User_Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        /// <summary>
        /// Gets or Sets role_id.
        /// </summary>
        [DataMember(Name = "role_id")]
        public int role_id { get; set; }


        /// <summary>
        /// Gets or Sets user_id.
        /// </summary>
        [DataMember(Name = "user_id")]
        public int user_id { get; set; }
    }
}
