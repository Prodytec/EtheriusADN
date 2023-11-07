using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

#pragma warning disable
namespace EtheriusWebAPI.Models.Entity
{

    [Table("master_menu_items")]
    public class Master_Menu_Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// Gets or Sets menu_item_name.
        /// </summary>
        [DataMember(Name = "menu_item_name")]
        public string? menu_item_name { get; set; }

        /// <summary>
        /// Gets or Sets parent_menu_item_id.
        /// </summary>
        [DataMember(Name = "parent_menu_item_id")]
        public int? parent_menu_item_id { get; set; }

        //[ForeignKey(nameof(id))]
        //public virtual User_Permissions Permissions { get; set; }

        //[ForeignKey("Id")]
        //public virtual Master_Menu_Items Parent_Menu_Items { get; set; }

        //[ForeignKey(nameof(parent_menu_item_id))]
        //public virtual Master_Menu_Items Parent_Menu { get; set; }
    }
}
