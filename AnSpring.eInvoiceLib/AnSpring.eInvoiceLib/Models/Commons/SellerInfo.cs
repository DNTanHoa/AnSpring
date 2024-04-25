using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Commons
{
    public class SellerInfo
    {
        public string SellerLegalName { get; set; }
        public string SellerTaxCode { get; set; }
        public string SellerAddressLine { get; set; }
        public string SellerPhoneNumber { get; set; }
        public string SellerFaxNumber { get; set; } 
        public string SellerEmail { get; set; }
        public string SellerBankName { get; set; }
        public string SellerBankAccount { get; set; }
        public string SellerDistrictName { get; set; }
        public string SellerCityName { get; set; }
        public string SellerCountryCode { get; set; }
        public string SellerWebsite { get; set; }
    }
}
