using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnSpring.eInvoiceLib.Models.Commons
{
    public class GeneralInvoiceInfo
    {
        /// <summary>
        /// Mã loại hóa đơn chỉ nhận các giá trị sau: 
        /// *******************************************
        /// Thông tư 32: 01GTKT, 02GTTT, 07KPTQ, 03XKNB, 04HGDL, 01BLP. 
        /// Tuân thủ theo quy định ký hiệu loại hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP. 
        /// Chi tiết xem PL1 Thông tư 39/2014/TT-BTC
        /// *******************************************
        /// Thông tư 78: 1, 2, 3, 4, 5, 6. Tuân thủ theo đúng Thông tư 78/2021/TT-BTC
        /// *******************************************
        /// Lưu ý: tại một thời điểm, doanh nghiệp có thể sử dụng nhiều loại hóa đơn.
        /// </summary>
        public string InvoiceType { get; set; }
        /// <summary>
        /// Ký hiệu mẫu hóa đơn, tuân thủ theo quy định ký hiệu mẫu hóa đơn của Thông tư hướng dẫn thi hành
        /// ******************************************
        /// Thông tư 32: Nghị định số 51/2010/NĐ-CP
        /// Ví dụ 01GTKT0/001, trong đó 
        /// 01GTKT: ký hiệu loại hóa đơn
        /// 0: số liên, đối với hóa đơn điện tử luôn là 0
        /// 001: số thứ tự tăng dần theo số lượng mẫu DN đăng ký với cơ quan thuế
        /// Chi tiết xem PL1 Thông tư 39/2014/TT-BTC
        /// ******************************************
        /// Thông tư 78: Ví dụ: 1/001 trong đó
        /// 1: Ký hiệu loại hóa đơn
        /// 001: Thứ tự tăng dần theo số lượng mẫu DN đăng ký với cơ quan thuế
        /// Chi tiết tại khoản 1, Điều 3 Thông tư 78/2019/TT-BTC
        /// Lưu ý: tại một thời điểm, doanh nghiệp có thể có nhiều mẫu hóa đơn.
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// Là “Ký hiệu hóa đơn” tuân thủ theo quy tắc tạo ký hiệu hóa đơn của Thông tư hướng dẫn thi hành
        /// ******************************************
        /// Thông tư 32: Nghị định số 51/2010/NĐ-CP. Ví dụ AA/20E
        /// Chi tiết xem PL1 Thông tư 39/2014/TT-BTC
        /// ******************************************
        /// Thông tư 78: Ví dụ: K20TYY
        /// Chi tiết tại khoản 1, Điều 3 Thông tư 78/2019/TT-BTC
        /// ******************************************
        /// Lưu ý: Tại một thời điểm, doanh nghiệp có thể có nhiều ký hiệu hóa đơn.
        /// Đối với hóa đơn theo TT78, người dùng không bắt buộc phải truyền đúng hai chữ số trong ký hiệu 
        /// theo đúng năm phát hành hóa đơn.
        /// Trường hợp người dùng truyền sai (năm quá khứ hoặc tương lai), hệ thống vẫn lưu ký hiệu theo năm
        /// Ví dụ: Người dùng lập hóa đơn với ký hiệu K18TAA, có ngày phát hành trong năm 2023, nếu truyền giá trị K50TAA, 
        /// hệ thống vẫn sẽ lưu ký hiệu hóa đơn sau khi lập là K23TAA.
        /// </summary>
        public string InvoiceSeries { get; set; }
        public DateTime? InvoiceIssuedDate { get; set; }
        public string CurrencyCode { get; set; }
        public string AdjustmentType { get; set; }
        public string AdjustedNote { get; set; }
        public string AdjustmentInvoiceType { get; set; }
        public string OriginalInvoiceId { get; set; }
        public long? OriginalInvoiceIssueDate { get; set; }
        public string AdditionalReferenceDesc { get; set; }
        public DateTime? AdditionalReferenceDate { get; set; }
        public bool? PaymentStatus { get; set; }
        public bool? CusGetInvoiceRight { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string TransactionUuid { get; set; }
        public string CertificateSerial { get; set; }
        public string OriginalInvoiceType { get; set; }
        public string OriginalTemplateCode { get; set; }
        public string ReservationCode { get; set; }
        public decimal? AdjustAmount20 { get; set; }
        public string InvoiceNote { get; set; }
        public decimal? Validation { get; set; }
    }
}
