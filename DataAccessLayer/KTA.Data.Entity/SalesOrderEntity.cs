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
    public class SalesOrderEntity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(SALESORDER.Columns.Id, Order = 0)]
        public int Id { get; set; }

        [Column(SALESORDER.Columns.SO, Order = 1)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string SO { get; set; }

        [Column(SALESORDER.Columns.Pn, Order = 2)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Pn { get; set; }

        [Column(SALESORDER.Columns.CustId, Order = 3)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string CustId { get; set; }

        [Column(SALESORDER.Columns.Qty, Order = 4)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public int Qty { get; set; }

        [Column(SALESORDER.Columns.Status, Order = 5)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Status { get; set; }

        [Column(SALESORDER.Columns.Creator, Order = 6)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Creator { get; set; }

        [Column(SALESORDER.Columns.Udt, Order = 7)]
        [NotNullValidator()]
        public DateTime Udt { get; set; }

        [Column(SALESORDER.Columns.Cdt, Order = 8)]
        [NotNullValidator()]
        public DateTime Cdt { get; set; }
    }
}
