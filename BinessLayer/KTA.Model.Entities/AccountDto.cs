using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Entities
{
    public class AccountDto
    {
        [Required]
        public string Account { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]            
        [RegularExpression(@"\d{2,4}-?\d{3,4}-?\d{3,4}(#\d{1,4})?", ErrorMessage = "Phone format is invalid")]
        public string Phone { get; set; }
        public DateTime Cdt { get; set; }
        public DateTime Udt { get; set; }
    }
}
