using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Entities
{
    public class CustomerDto
    {
        [Required]
        public string CustId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^09\d{2}\-?\d{3}\-?\d{3}$")]        
        [RegularExpression(@"\d{2,4}-?\d{3,4}-?\d{3,4}(#\d{1,4})?", ErrorMessage="Phone format is invalid")]     
        public string Phone { get; set; }           
        public DateTime Cdt { get; set; }
        public DateTime Udt { get; set; }
    }
}
