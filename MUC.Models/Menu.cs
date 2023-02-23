using MUC.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace MUC.Models
{
    public class Menu
    {
        [Key]
        public Guid ID { get; set; }


        [PastDate]
        public DateOnly DateColumn { get; set; }

        public ICollection<ProductMenu>? ProductMenus { get; set; }

    }
}
