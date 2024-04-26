using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Requests
{
    public class LookupInvoiceRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceType { get; set; }
        public int RowPerPage { get; set; }
        public int PageNum { get; set; }
        public string TemplateCode { get; set; }
    }
}
