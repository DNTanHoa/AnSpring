using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Requests
{
    public class CancelInvoiceRequest
    {
        public string SupplierTaxCode { get; set; }
        public string InvoiceNo { get; set; }
        public string TemplateCode { get; set; }
        public long StrIssueDate { get; set; }
        public string AdditionalReferenceDesc { get; set; }
        public long AdditionalReferenceDate { get; set; }
        public string ReasonDelete { get; set; }
    }
}
