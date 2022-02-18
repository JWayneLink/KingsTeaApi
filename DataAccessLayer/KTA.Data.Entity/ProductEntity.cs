using KTA.Data.Schema;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Entity
{
    public class ProductEntity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(PRODUCT.Columns.Id, Order = 0)]
        public int Id { get; set; }

        [Column(PRODUCT.Columns.Pn, Order = 1)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Pn { get; set; }

        [Column(PRODUCT.Columns.Name, Order = 2)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Name { get; set; }

        [Column(PRODUCT.Columns.Category, Order = 3)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Category { get; set; }

        [Column(PRODUCT.Columns.Size, Order = 4)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Size { get; set; }

        [Column(PRODUCT.Columns.Price, Order = 5)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public decimal Price { get; set; }

        [Column(PRODUCT.Columns.Kcal, Order = 6)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public int Kcal { get; set; }

        [Column(PRODUCT.Columns.COO, Order = 7)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string COO { get; set; }

        [Column(PRODUCT.Columns.Udt, Order = 8)]
        [NotNullValidator()]
        public DateTime Udt { get; set; }

        [Column(PRODUCT.Columns.Cdt, Order = 9)]
        [NotNullValidator()]
        public DateTime Cdt { get; set; }
    }
}
