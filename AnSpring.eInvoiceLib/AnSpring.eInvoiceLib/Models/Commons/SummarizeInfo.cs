using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Commons
{
    /// <summary>
    /// Dùng để tổng hợp tiền hàng cho toàn bộ hóa đơn.
    /// Không sử dụng, hệ thống tự tính tổng tiền hóa đơn
    /// </summary>
    public class SummarizeInfo
    {
        public decimal SumOfTotalLineAmountWithoutTax { get; set; }
        public decimal TotalAmountWithoutTax { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public decimal TotalAmountWithTaxFrn { get; set; }
        public string TotalAmountWithTaxInWords { get; set; }
        public bool IsTotalAmountPos { get; set; }
        public bool IsTotalTaxAmountPos { get; set; }
        public bool IsTotalAmtWithoutTaxPos { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SettlementDiscountAmount { get; set; }
        public bool IsDiscountAmtPos { get; set; }

    }
}
