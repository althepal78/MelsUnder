using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MUC.Models.Validations;

namespace MUC.Models.ViewModels {
    public class MenuVM {
        public Guid ProductId { get; set; }
        public Guid MenuId { get; set; }

        [PastDate]
        public DateTime DateColumn { get; set; }

        [ValidateNever]
        public Product? OneProduct { get; set; }
        public List<Product>? Products { get; set; }
        public ICollection<ProductMenu>? ProductMenu { get; set; }
        public List<Menu>? Menus { get; set; }
    }
}
