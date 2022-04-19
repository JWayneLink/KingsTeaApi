using KTA.Data.Schema;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Entity
{
    [Table("PRODUCT", Schema = "dbo")]
    public class ProductEntity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(PRODUCT.Columns.Id, Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(PRODUCT.Columns.Pn, Order = 1, TypeName = "varchar(100)")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Pn { get; set; }

        [Column(PRODUCT.Columns.Name, Order = 2, TypeName = "varchar(100)")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Name { get; set; }

        [Column(PRODUCT.Columns.Category, Order = 3, TypeName = "varchar(100)")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Category { get; set; }

        [Column(PRODUCT.Columns.Size, Order = 4, TypeName = "varchar(100)")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Size { get; set; }

        [Column(PRODUCT.Columns.Sugar, Order = 5, TypeName = "varchar(100)")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Sugar { get; set; }

        [Column(PRODUCT.Columns.Ice, Order = 6, TypeName = "varchar(100)")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Ice { get; set; }

        [Column(PRODUCT.Columns.Price, Order = 7, TypeName = "money")]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public decimal Price { get; set; }

        [Column(PRODUCT.Columns.Udt, Order = 8, TypeName = "datetime")]
        [NotNullValidator()]
        public DateTime Udt { get; set; }

        [Column(PRODUCT.Columns.Cdt, Order = 9, TypeName = "datetime")]
        [NotNullValidator()]
        public DateTime Cdt { get; set; }
    }
}
