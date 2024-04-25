using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Responses
{
    public class ZipFileResponse
    {
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
