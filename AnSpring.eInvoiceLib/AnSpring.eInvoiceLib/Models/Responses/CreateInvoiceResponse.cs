using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Responses
{
    public class CreateInvoiceResponse
    {
        public string SupplierTaxCode { get; set; }
        public string InvoiceNo { get; set; }
        public string TransactionID { get; set; }
        public string ReservationCode { get; set; }
        public string CodeOfTax { get; set; }
    }
}
