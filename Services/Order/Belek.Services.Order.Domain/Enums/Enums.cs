using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belek.Services.Order.Domain.Enums
{
    public enum OrderStatus
    {
        New,
        Processing,
        Done,
        Cancel,
        Suspend
    }
}
