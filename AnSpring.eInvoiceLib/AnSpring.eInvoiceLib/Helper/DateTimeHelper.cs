using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Helper
{
    public class DateTimeHelper
    {
        public static long GetMiliSecond(DateTime dateTime)
        {
            var fromDate = new DateTime(1970, 01, 01);
            return (long)((dateTime - fromDate).TotalMilliseconds);
        }
    }
}
