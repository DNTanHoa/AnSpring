using AnSpring.eInvoiceLib.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Requests
{
    public class CreateInvoiceRequest
    {
        public GeneralInvoiceInfo GeneralInvoiceInfo { get; set; }
        public SellerInfo SellerInfo { get; set; }
        public BuyerInfo BuyerInfo { get; set; }
        public List<Payments> Payments { get; set; }
        public List<TaxBreakDowns> TaxBreakDowns { get; set; }
        public List<ItemInfo> ItemInfo { get; set; }
    }
}
