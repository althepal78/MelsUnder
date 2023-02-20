
using MUC.Models.Validations;

namespace MUC.Models.ViewModels
{
    public class MenuVM
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }

        [PastDate]
        public DateTime DateColumn { get; set; }
        public List<Product>? Products { get; set; }
        public ICollection<ProductMenu>? ProductMenu { get; set; }
        public List<Menu>? Menus { get; set; }
    }
}
