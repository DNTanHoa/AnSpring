using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Responses
{
    public class LookupInvoiceResponse
    {
        public long IvoiceId { get; set; }
        public string InvoiceType { get; set; }
        public string AdjustmentType { get; set; }
        public string TemplateCode { get; set; }
        public string InvoiceSeri { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceNo { get; set; }
        public string Currency { get; set; }
        public decimal? Total { get; set; }
        public string IssueDate { get; set; }
        public DateTime? IssueDateStr { get; set; }
        public int? State { get; set; }
        public string RequestDate { get; set; }
        public string Description { get; set; }
        public string BuyerIdNo { get; set; }
        public int? StateCode { get; set; }
        public string SubscriberNumber { get; set; }
        public int? PaymentStatus { get; set; }
        public string ViewStatus { get; set; }
        public string DownloadStatus { get; set; }
        public int? ExchangeStatus { get; set; }
        public string NumOfExchange { get; set; }
        public string CreateTime { get; set; }
        public string ContractId { get; set; }
        public string ContractNo { get; set; }
        public string SupplierTaxCode { get; set; }
        public string BuyerTaxCode { get; set; }
        public decimal? TotalBeforeTax { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxRate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTime { get; set; }
        public string CustomerId { get; set; }
        public string No { get; set; }
        public string PaymentStatusName { get; set; }
        public string BuyerName { get; set; }
        public string TransactionUuid { get; set; }
    }
}
