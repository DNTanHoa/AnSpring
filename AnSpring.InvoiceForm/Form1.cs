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

        }
    }
}
