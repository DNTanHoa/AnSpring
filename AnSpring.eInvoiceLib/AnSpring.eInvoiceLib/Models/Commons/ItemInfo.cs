using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Commons
{
    public class ItemInfo
    {
        public string LineNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string ItemTotalAmountWithoutTax { get; set; }
        public int TaxPercentage { get; set; }
        public string TaxAmount { get; set; }
        public string Discount { get; set; }
        public string ItemDiscount { get; set; }
        public string AdjustmentTaxAmount { get; set; }
        public string IsIncreaseItem { get; set; }
    }
}
