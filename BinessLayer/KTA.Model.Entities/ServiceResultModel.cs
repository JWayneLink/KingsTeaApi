using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Entities
{
    public class ServiceResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
}
