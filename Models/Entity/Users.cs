using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

#pragma warning disable

namespace EtheriusWebAPI.Models.Entity
{

    [Table("users")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        /// <summary>
        /// Gets or Sets first_name.
        /// </summary>
        [DataMember(Name = "first_name")]
        public string? first_name { get; set; }


        /// <summary>
        /// Gets or Sets last_name.
        /// </summary>
        [DataMember(Name = "last_name")]
        public string? last_name { get; set; }

        /// <summary>
        /// Gets or Sets email.
        /// </summary>
        [DataMember(Name = "email")]
        public string? email { get; set; }


        /// <summary>
        /// Gets or Sets password.
        /// </summary>
        [DataMember(Name = "password")]
        public string? password { get; set; }


        /// <summary>
        /// Gets or Sets is_active.
        /// </summary>
        [DataMember(Name = "is_active")]
        public int is_active { get; set; }


        /// <summary>
        /// Gets or Sets created_date.
        /// </summary>
        [DataMember(Name = "created_date")]
        public DateTime created_date { get; set; }

        //[ForeignKey(nameof(id))]
        //public virtual ICollection<User_Roles> User_Roles { get; set; }
    }
}
