using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using KTA.Data.Schema;

namespace KTA.Data.Entity
{
    public class AccountEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(ACCOUNT.Columns.Id, Order = 0)]
        public int Id { get; set; }

        [Column(ACCOUNT.Columns.Account, Order = 1)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Account { get; set; }

        [Column(ACCOUNT.Columns.Name, Order = 2)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Name { get; set; }

        [Column(ACCOUNT.Columns.Pwd, Order = 3)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Pwd { get; set; }

        [Column(ACCOUNT.Columns.Email, Order = 4)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Email { get; set; }

        [Column(ACCOUNT.Columns.Phone, Order = 5)]
        [NotNullValidator()]
        [StringLengthValidator(1, 100)]
        public string Phone { get; set; }
        
        [Column(ACCOUNT.Columns.Udt, Order = 6)]
        [NotNullValidator()]
        public DateTime Udt { get; set; }

        [Column(ACCOUNT.Columns.Cdt, Order = 7)]
        [NotNullValidator()]
        public DateTime Cdt { get; set; }
    }    
}
