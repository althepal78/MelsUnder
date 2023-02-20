using Microsoft.EntityFrameworkCore;
using MUC.Models.Validations;

namespace MUC.Models
{
    [Keyless]
    public class ProductMenu
    {
        public Product Product { get; set; }
        public Guid ProductID { get; set; }
        public Menu Menu { get; set; }
        public Guid MenuID { get; set; }
    }
}
