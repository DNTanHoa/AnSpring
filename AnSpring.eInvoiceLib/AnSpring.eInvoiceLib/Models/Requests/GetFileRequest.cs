using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Requests
{
    public class GetFileRequest
    {
        public string InvoiceNo { get; set; }
        public string FileType { get; set; }
        public string StrIssueDate { get; set; }
        public string AdditionalReferenceDesc { get; set; }
        public string AdditionalReferenceDate { get; set; }
        public string Pattern { get; set; }
        public string TransactionUuid { get; set; }
    }
}
