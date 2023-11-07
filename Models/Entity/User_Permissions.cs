using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

#pragma warning disable

namespace EtheriusWebAPI.Models.Entity
{
    [Table("user_permissions")]
    public class User_Permissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// Gets or Sets enable.
        /// </summary>
        [DataMember(Name = "enable")]
        public int enable { get; set; }


        /// <summary>
        /// Gets or Sets menu_item_id.
        /// </summary>
        [DataMember(Name = "menu_item_id")]
        public int menu_item_id { get; set; }


        /// <summary>
        /// Gets or Sets role_id.
        /// </summary>
        [DataMember(Name = "role_id")]
        public int role_id { get; set; }
    }
}
