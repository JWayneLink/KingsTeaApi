using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Entities
{
    public class ProductDto
    {
        [Required]
        public string Pn { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Kcal { get; set; }
        public string COO { get; set; }
        public DateTime Cdt { get; set; }
        public DateTime Udt { get; set; }
    }
}
