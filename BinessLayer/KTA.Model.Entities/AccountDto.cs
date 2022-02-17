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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pwd { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
