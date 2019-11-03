using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiroty.Models
{
    public static class Enums
    {
        public enum Role
        {
            SuperAdmin,
            Admin,
            Trader,
            User
        }

        public enum TypeProduct
        {
            FOREX,
            OPTION,
            FUTURE,
            BOND,
            EQUITY
        }

        public enum StatusDeal
        {
            Cancelled,
            Opened,
            Closed
        }

        public enum Way
        {
            BUY,
            SELL
        }
    }
}
