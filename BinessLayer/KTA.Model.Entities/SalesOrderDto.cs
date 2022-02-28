using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Entities
{
    public class SalesOrderDto
    {
        public string SO { get; set; }
        public string Pn { get; set; }
        public string CustId { get; set; }
        public int Qty { get; set; }
        public string Status { get; set; }
        public string Creator { get; set; }
        public DateTime Cdt { get; set; }
        public DateTime Udt { get; set; }
    }
}
