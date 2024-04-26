using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnSpring.eInvoiceLib.Models.Requests;
using AnSpring.eInvoiceLib.Models.Commons;
using AnSpring.eInvoiceLib.Models.Responses;
using AnSpring.eInvoiceLib.Lib;
using System.IO;
using AnSpring.eInvoiceLib.Helper;

namespace AnSpring.InvoiceForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Phát hành hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị cấu hình
                string passWord = ConfigurationManager.AppSettings["UserPass"].ToString();
                string userName = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString() 
                    + @"/InvoiceAPI/InvoiceWS/createInvoice/" + userName;

                // Khai báo đối tượng hóa đơn
                CreateInvoiceRequest request = new CreateInvoiceRequest() 
                {
                    GeneralInvoiceInfo = new GeneralInvoiceInfo()
                    {
                        InvoiceType = "01GTKT",
                        TemplateCode = "01GTKT0/135",
                        InvoiceSeries = "AB/20E",
                        CurrencyCode = "VND",
                        AdjustmentType = "1",
                        PaymentStatus = true,
                        CusGetInvoiceRight = true,
                        InvoiceIssuedDate = DateTimeHelper.GetMiliSecond(DateTime.UtcNow).ToString(),
                        TransactionUuid = Guid.NewGuid().ToString(),
                    },
                    SellerInfo = new SellerInfo()
                    {
                        SellerLegalName = "Dương Nguyễn Tấn Hòa",
                        SellerTaxCode = userName,
                        SellerAddressLine = "xã Phước Lợi, huyện Bến Lức, tỉnh Long An",
                        SellerPhoneNumber = "0359759402",
                        SellerFaxNumber = "0359759402",
                        SellerEmail = "tanhoatm@gmail.com",
                        SellerBankName = "Ngân hàng thương mại cổ phần Phương Đông - OCB",
                        SellerBankAccount = "0056100001725006",
                        SellerCityName = "Thành phố Hồ Chí Minh",
                        SellerDistrictName = "Quận 11",
                        SellerCountryCode = "84",
                        SellerWebsite = "cafedevcode.com"
                    },
                    BuyerInfo = new BuyerInfo()
                    {
                        BuyerLegalName = "Dương Nguyễn Tấn Hòa",
                        BuyerTaxCode = userName,
                        BuyerAddressLine = "xã Phước Lợi, huyện Bến Lức, tỉnh Long An",
                        BuyerPhoneNumber = "0359759402",
                        BuyerFaxNumber = "0359759402",
                        BuyerEmail = "tanhoatm@gmail.com",
                        BuyerBankName = "Ngân hàng thương mại cổ phần Phương Đông - OCB",
                        BuyerBankAccount = "0056100001725006",
                        BuyerCityName = "Thành phố Hồ Chí Minh",
                        BuyerDistrictName = "Quận 11",
                        BuyerCountryCode = "84",
                        BuyerIdType = "3",
                        BuyerIdNo = "8888899999"
                    },
                    Payments = new List<Payments>()
                    {
                        new Payments()
                        {
                            PaymentMethodName = "CHuyển khoản"
                        }
                    },
                    TaxBreakDowns = new List<TaxBreakDowns>() 
                    {
                        new TaxBreakDowns()
                        {
                            TaxPercentage = 10,
                            TaxableAmount = 3952730,
                            TaxAmount = 395273,
                        }
                    },
                    ItemInfo = new List<ItemInfo>() 
                    {
                        new ItemInfo()
                        {
                            LineNumber = 1,
                            ItemCode = "HH001",
                            ItemName = "Hàng hóa 001",
                            UnitName = "Chiếc",
                            UnitPrice = 150450,
                            Quantity = 10,
                            ItemTotalAmountWithoutTax = 1504500,
                            TaxPercentage = 10,
                            TaxAmount = 150450
                        },
                        new ItemInfo()
                        {
                            LineNumber = 2,
                            ItemCode = "HH002",
                            ItemName = "Hàng hóa 002",
                            UnitName = "Chiếc",
                            UnitPrice = 150450,
                            Quantity = 10,
                            ItemTotalAmountWithoutTax = 1504500,
                            TaxPercentage = 10,
                            TaxAmount = 150450
                        }
                    }
                };

                // Gọi api là chờ nhận kết quả
                (bool result, string errorCode, string message, CreateInvoiceResponse responseData)
                    = InvoiceClient.CreateInvoiceAsync(apiLink, userName, passWord,
                    request);

                if(result)
                {
                    MessageBox.Show($"Tích hợp thành công: Số Hóa Đơn {responseData.InvoiceNo} - Mã Giao Dịch: {responseData.TransactionID}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Có lỗi xảy ra: Mã lỗi {errorCode} - Nội dung: {message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Lấy file zip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị cấu hình
                string passWord = ConfigurationManager.AppSettings["UserPass"].ToString();
                string userName = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString()
                    + @"/InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile";

                // Khai báo đối tượng get file
                GetFileRequest request = new GetFileRequest()
                {
                    SupplierTaxCode = userName,
                    FileType = "ZIP",
                    InvoiceNo = txtInvoiceNo.Text,
                    TemplateCode = txtTemplateCode.Text,
                };

                (bool result, string errorCode, string message, ZipFileResponse responseData)
                    = InvoiceClient.GetZipFile(apiLink, userName, passWord, request);

                if (result)
                {
                    // Xử lý lưu file
                    if (!Directory.Exists(Path.Combine(Application.StartupPath, "ZipFile")))
                        Directory.CreateDirectory(Path.Combine(Application.StartupPath, "ZipFile"));
                    
                    string fileName = Path.GetFileNameWithoutExtension(responseData.FileName) + ".zip";
                    string saveFileName = GetUniqueFileName(Path.Combine(Application.StartupPath, "ZipFile"), fileName);

                    string path = Path.Combine(Application.StartupPath, "ZipFile") + "\\" + saveFileName;
                    byte[] data = Convert.FromBase64String(responseData.FileToBytes);
                    File.WriteAllBytes(path, data);

                    // Thông báo kết quả
                    MessageBox.Show($"Lấy file thành công: {path}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Có lỗi xảy ra: Mã lỗi {errorCode} - Nội dung: {message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(Exception ex )
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lập hóa đơn điều chỉnh tiền
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hủy hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị cấu hình
                string passWord = ConfigurationManager.AppSettings["UserPass"].ToString();
                string userName = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString()
                    + @"/InvoiceAPI/InvoiceWS/cancelTransactionInvoice";

                // Khai báo đối tượng hủy hóa đơn
                CancelInvoiceRequest request = new CancelInvoiceRequest()
                {
                    SupplierTaxCode = userName,
                    StrIssueDate = DateTimeHelper.GetMiliSecond(issuedDatePicker.Value.ToUniversalTime()),
                    AdditionalReferenceDate = DateTimeHelper.GetMiliSecond(DateTime.Today),
                    AdditionalReferenceDesc = "TEST",
                    InvoiceNo = txtInvoiceNo.Text,
                    TemplateCode = txtTemplateCode.Text,
                    ReasonDelete = "TEST"
                };

                (bool result, string errorCode, string message, CancelInvoiceRespone responseData)
                    = InvoiceClient.CancelInvoice(apiLink, userName, passWord, request);

                if(result)
                {
                    MessageBox.Show($"Hủy thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Có lỗi xảy ra: Mã lỗi {errorCode} - Nội dung: {message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy file pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị cấu hình
                string passWord = ConfigurationManager.AppSettings["UserPass"].ToString();
                string userName = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString()
                    + @"/InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile";

                // Khai báo đối tượng get file
                GetFileRequest request = new GetFileRequest()
                {
                    SupplierTaxCode = userName,
                    FileType = "PDF",
                    InvoiceNo = txtInvoiceNo.Text,
                    TemplateCode = txtTemplateCode.Text,
                };

                (bool result, string errorCode, string message, ZipFileResponse responseData)
                    = InvoiceClient.GetZipFile(apiLink, userName, passWord, request);

                if (result)
                {
                    // Xử lý lưu file
                    if (!Directory.Exists(Path.Combine(Application.StartupPath, "PdfFile")))
                        Directory.CreateDirectory(Path.Combine(Application.StartupPath, "PdfFile"));

                    string fileName = Path.GetFileNameWithoutExtension(responseData.FileName) + ".pdf";
                    string saveFileName = GetUniqueFileName(Path.Combine(Application.StartupPath, "PdfFile"), fileName);

                    string path = Path.Combine(Application.StartupPath, "PdfFile") + "\\" + saveFileName;
                    byte[] data = Convert.FromBase64String(responseData.FileToBytes);
                    File.WriteAllBytes(path, data);

                    // Thông báo kết quả
                    MessageBox.Show($"Lấy file thành công: {path}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Có lỗi xảy ra: Mã lỗi {errorCode} - Nội dung: {message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tra cứu hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị cấu hình
                string passWord = ConfigurationManager.AppSettings["UserPass"].ToString();
                string userName = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString()
                    + @"/InvoiceAPI/InvoiceUtilsWS/getInvoices/" + userName;

                // Khai báo đối tượng lookup
                LookupInvoiceRequest request = new LookupInvoiceRequest()
                {
                    StartDate = DateTime.Today.AddDays(-2),
                    EndDate = DateTime.Today.AddDays(1),
                    PageNum = 1,
                    RowPerPage = 50,
                    TemplateCode = null,
                    InvoiceType = null
                };

                (bool result, string errorCode, string message, int totalRows, List<LookupInvoiceResponse> responseData)
                    = InvoiceClient.LookupInvoices(apiLink, userName, passWord, request);

                if(result)
                {
                    MessageBox.Show($"Thành công: Tổng số hóa đơn {totalRows}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Có lỗi xảy ra: {message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Support
        static string GetUniqueFileName(string directoryPath, string fileName)
        {
            string newFileName = fileName;
            string extension = Path.GetExtension(fileName); // Lấy phần mở rộng của tên tệp

            int counter = 1;
            while (File.Exists(Path.Combine(directoryPath, newFileName))) // Kiểm tra xem tệp đã tồn tại chưa
            {
                newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + counter + extension; // Tăng số lên và thêm vào tên
                counter++;
            }

            return newFileName;
        }

        #endregion

        /// <summary>
        /// Tạo hóa đơn nháp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị cấu hình
                string passWord = ConfigurationManager.AppSettings["UserPass"].ToString();
                string userName = ConfigurationManager.AppSettings["CodeTax"].ToString();
                string apiLink = ConfigurationManager.AppSettings["APILink"].ToString()
                    + @"/InvoiceAPI/InvoiceWS/createOrUpdateInvoiceDraft/" + userName;

                // Khai báo đối tượng hóa đơn
                CreateInvoiceRequest request = new CreateInvoiceRequest()
                {
                    GeneralInvoiceInfo = new GeneralInvoiceInfo()
                    {
                        InvoiceType = "01GTKT",
                        TemplateCode = "01GTKT0/135",
                        InvoiceSeries = "AB/20E",
                        CurrencyCode = "VND",
                        AdjustmentType = "1",
                        PaymentStatus = true,
                        CusGetInvoiceRight = true,
                        InvoiceIssuedDate = DateTimeHelper.GetMiliSecond(DateTime.UtcNow).ToString(),
                        TransactionUuid = Guid.NewGuid().ToString(),
                    },
                    SellerInfo = new SellerInfo()
                    {
                        SellerLegalName = "Dương Nguyễn Tấn Hòa",
                        SellerTaxCode = userName,
                        SellerAddressLine = "xã Phước Lợi, huyện Bến Lức, tỉnh Long An",
                        SellerPhoneNumber = "0359759402",
                        SellerFaxNumber = "0359759402",
                        SellerEmail = "tanhoatm@gmail.com",
                        SellerBankName = "Ngân hàng thương mại cổ phần Phương Đông - OCB",
                        SellerBankAccount = "0056100001725006",
                        SellerCityName = "Thành phố Hồ Chí Minh",
                        SellerDistrictName = "Quận 11",
                        SellerCountryCode = "84",
                        SellerWebsite = "cafedevcode.com"
                    },
                    BuyerInfo = new BuyerInfo()
                    {
                        BuyerLegalName = "Dương Nguyễn Tấn Hòa",
                        BuyerTaxCode = userName,
                        BuyerAddressLine = "xã Phước Lợi, huyện Bến Lức, tỉnh Long An",
                        BuyerPhoneNumber = "0359759402",
                        BuyerFaxNumber = "0359759402",
                        BuyerEmail = "tanhoatm@gmail.com",
                        BuyerBankName = "Ngân hàng thương mại cổ phần Phương Đông - OCB",
                        BuyerBankAccount = "0056100001725006",
                        BuyerCityName = "Thành phố Hồ Chí Minh",
                        BuyerDistrictName = "Quận 11",
                        BuyerCountryCode = "84",
                        BuyerIdType = "3",
                        BuyerIdNo = "8888899999"
                    },
                    Payments = new List<Payments>()
                    {
                        new Payments()
                        {
                            PaymentMethodName = "CHuyển khoản"
                        }
                    },
                    TaxBreakDowns = new List<TaxBreakDowns>()
                    {
                        new TaxBreakDowns()
                        {
                            TaxPercentage = 10,
                            TaxableAmount = 3952730,
                            TaxAmount = 395273,
                        }
                    },
                    ItemInfo = new List<ItemInfo>()
                    {
                        new ItemInfo()
                        {
                            LineNumber = 1,
                            ItemCode = "HH001",
                            ItemName = "Hàng hóa 001",
                            UnitName = "Chiếc",
                            UnitPrice = 150450,
                            Quantity = 10,
                            ItemTotalAmountWithoutTax = 1504500,
                            TaxPercentage = 10,
                            TaxAmount = 150450
                        },
                        new ItemInfo()
                        {
                            LineNumber = 2,
                            ItemCode = "HH002",
                            ItemName = "Hàng hóa 002",
                            UnitName = "Chiếc",
                            UnitPrice = 150450,
                            Quantity = 10,
                            ItemTotalAmountWithoutTax = 1504500,
                            TaxPercentage = 10,
                            TaxAmount = 150450
                        }
                    }
                };

                // Gọi api là chờ nhận kết quả
                bool result = InvoiceClient.CreateDrafInvoice(apiLink, userName, passWord, request,
                    out string errorCode, out string message, out CreateInvoiceResponse responseData);

                if (result)
                {
                    MessageBox.Show($"Tích hợp thành công: Số Hóa Đơn {responseData.InvoiceNo} - Mã Giao Dịch: {responseData.TransactionID}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Có lỗi xảy ra: Mã lỗi {errorCode} - Nội dung: {message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
