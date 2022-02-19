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
    public  class CustomerEntity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(CUSTOMER.Columns.Id, Order = 0)]
        public int Id { get; set; }

        [Column(CUSTOMER.Columns.CustId, Order = 1)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string CustId { get; set; }

        [Column(CUSTOMER.Columns.Name, Order = 2)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Name { get; set; }

        [Column(CUSTOMER.Columns.Title, Order = 3)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Title { get; set; }

        [Column(CUSTOMER.Columns.Address, Order = 4)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Address { get; set; }

        [Column(CUSTOMER.Columns.Phone, Order = 5)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Phone { get; set; }

        [Column(CUSTOMER.Columns.Udt, Order = 6)]
        [NotNullValidator()]
        public DateTime Udt { get; set; }

        [Column(CUSTOMER.Columns.Cdt, Order = 7)]
        [NotNullValidator()]
        public DateTime Cdt { get; set; }
    }
}
