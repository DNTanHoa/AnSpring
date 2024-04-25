﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Requests
{
    public class GetFileRequest
    {
        public string SupplierTaxCode { get; set; }
        public string InvoiceNo { get; set; }
        public string FileType { get; set; }
        public string TemplateCode { get; set; }
        //public string TransactionUuid { get; set; }
    }
}
