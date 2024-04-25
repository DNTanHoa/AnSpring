using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Commons
{
    public class ItemInfo
    {
        public int LineNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemTotalAmountWithoutTax { get; set; }
        public int TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? ItemDiscount { get; set; }
        public decimal? AdjustmentTaxAmount { get; set; }
        public bool? IsIncreaseItem { get; set; }
    }
}
