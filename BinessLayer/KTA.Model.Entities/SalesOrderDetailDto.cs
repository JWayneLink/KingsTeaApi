using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Entities
{    
    public class SalesOrderDetailDto
    {
        public string SO { get; set; }
        public string CustName { get; set; }
        public string CustTitle { get; set; }
        public string CustAddress { get; set; }
        public string CustPhone { get; set; }
        public string Status { get; set; }
        public string Creator { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Cdt { get; set; }
        public DateTime Udt { get; set; }
        public List<OrderDetailDto> OrderDetailDtos { get; set; }
    }

    public class OrderDetailDto
    {       
        // Product Info
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        public string ProductSugar { get; set; }
        public string ProductIce { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQty { get; set; }
        public decimal ProductSubTotal { get; set; }
    }
}
