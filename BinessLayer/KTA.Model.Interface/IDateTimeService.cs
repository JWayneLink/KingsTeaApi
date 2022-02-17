using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface IDateTimeService : IService
    {
        DateTime GetCurrentTime();
    }
}
