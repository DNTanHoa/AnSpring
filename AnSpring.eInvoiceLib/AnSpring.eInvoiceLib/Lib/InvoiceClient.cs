using AnSpring.eInvoiceLib.Models.Requests;
using AnSpring.eInvoiceLib.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AnSpring.eInvoiceLib.Lib
{
    public class InvoiceClient
    {
        /// <summary>
        /// Hàm phát hành, thay thế, điều chỉnh hóa đơn
        /// </summary>
        /// <param name="url">Địa chỉ toàn vẹn API</param>
        /// <param name="userName">Mã số thuế</param>
        /// <param name="password">Mật khẩu</param>
        /// <param name="requestData">Request xuất hóa đơn</param>
        /// <returns>
        /// bool: Tích hợp thành công hay thất bại
        /// string: Mã lỗi
        /// string: Mô tả lỗi
        /// CreateInvoiceResponse: Kết quả nhận được trong trường hợp thành công
        /// </returns>
        public static (bool, string, string, CreateInvoiceResponse) CreateInvoiceAsync(
            string url, string userName, string passWord, CreateInvoiceRequest requestData)
        {
            bool result = false;
            string errorCode = string.Empty, message = string.Empty;
            CreateInvoiceResponse responseData = new CreateInvoiceResponse();

            using(var client = new HttpClient())
            {
                string basic = CreateBasicAuthHeader(userName, passWord);
                client.BaseAddress = new Uri(url);
                

                // Serialize đối tượng thành chuỗi JSON
                string jsonData = JsonConvert.SerializeObject(requestData, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });

                // Tạo HttpRequestMessage với phương thức POST và dữ liệu JSON
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                //requestMessage.Headers.Add("Content-Type", "application/json");
                requestMessage.Headers.Add("Authorization", basic);

                // Gửi yêu cầu và nhận phản hồi
                HttpResponseMessage response = client.SendAsync(requestMessage).Result;

                // Xử lý phản hồi
                if(response.IsSuccessStatusCode)
                {
                    // Đọc nội dung của phản hồi
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Phân tích phản hồi JSON
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    // Kiểm tra nếu có trường "result"
                    if (responseObject.result != null)
                    {
                        // Xử lý phản hồi thành công
                        responseData = JsonConvert.DeserializeObject<CreateInvoiceResponse>(responseObject.result.ToString());

                        // Gán kết quả xử lý là true
                        result = true;
                    }
                    else if (responseObject.code != null)
                    {
                        // Xử lý phản hồi lỗi
                        errorCode = responseObject.code;
                        message = responseObject.message;
                    }
                    else
                    {
                        // Phản hồi không xác định
                        errorCode = "UNKNOWN";
                        message = "Lỗi không xác định";
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Đọc nội dung của phản hồi
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Phân tích phản hồi JSON
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    // Xử lý phản hồi lỗi
                    errorCode = responseObject.code;
                    message = responseObject.message;
                }
                else
                {
                    result = false;
                    errorCode = response.StatusCode.ToString();
                }
            }

            return (result, errorCode, message, responseData);
        }

        /// <summary>
        /// Hàm tải về hóa đơn
        /// </summary>
        /// <param name="url">Địa chỉ toàn vẹn API</param>
        /// <param name="userName">Mã số thuế</param>
        /// <param name="passWord">Mật khẩu</param>
        /// <param name="requestData">Request tải file</param>
        /// <returns>
        /// bool: Tích hợp thành công hay thất bại
        /// string: Mã lỗi
        /// string: Mô tả lỗi
        /// ZipFileResponse: Kết quả nhận được trong trường hợp thành công
        /// </returns>
        public static (bool, string, string, ZipFileResponse) GetZipFile(
            string url, string userName, string passWord, GetFileRequest requestData)
        {
            bool result = false;
            string errorCode = string.Empty, message = string.Empty;
            ZipFileResponse responseData = new ZipFileResponse();

            using(var client = new HttpClient())
            {
                string basic = CreateBasicAuthHeader(userName, passWord);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Authorization", basic);

                // Serialize đối tượng thành chuỗi JSON
                string jsonData = JsonConvert.SerializeObject(requestData, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });

                // Tạo HttpRequestMessage với phương thức POST và dữ liệu JSON
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Gửi yêu cầu và nhận phản hồi
                HttpResponseMessage response = client.SendAsync(requestMessage).Result;

                // Xử lý phản hồi
                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung của phản hồi
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Phân tích phản hồi JSON
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    // Xử lý phản hồi thành công
                    responseData = JsonConvert.DeserializeObject<ZipFileResponse>(responseContent);

                    // Gán kết quả xử lý là true
                    result = true;
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Đọc nội dung của phản hồi
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Phân tích phản hồi JSON
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    // Xử lý phản hồi lỗi
                    errorCode = responseObject.code;
                    message = responseObject.message;
                }
                else
                {
                    result = false;
                    errorCode = response.StatusCode.ToString();
                }
            }

            return (result, errorCode, message, responseData);
        }

        /// <summary>
        /// Hàm hủy hóa đơn
        /// </summary>
        /// <param name="url">Địa chỉ toàn vẹn API</param>
        /// <param name="userName">Mã số thuế</param>
        /// <param name="passWord">Mật khẩu</param>
        /// <param name="requestData">Request hủy hóa đơn</param>
        /// <returns>
        /// bool: Tích hợp thành công hay thất bại
        /// string: Mã lỗi
        /// string: Mô tả lỗi
        /// CancelInvoiceRespone: Kết quả hủy hóa đơn
        /// </returns>
        public static (bool, string, string, CancelInvoiceRespone) CancelInvoice(
            string url, string userName, string passWord, CancelInvoiceRequest requestData)
        {
            bool result = false;
            string errorCode = string.Empty, message = string.Empty;
            CancelInvoiceRespone responseData = new CancelInvoiceRespone();

            using(HttpClient client = new HttpClient())
            {
                string basic = CreateBasicAuthHeader(userName, passWord);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Authorization", basic);

                // Tạo HttpRequestMessage với phương thức POST và dữ liệu Form
                var formData = new Dictionary<string, string>
                {
                    { "supplierTaxCode", requestData.SupplierTaxCode },
                    { "invoiceNo", requestData.InvoiceNo },
                    { "templateCode", requestData.TemplateCode },
                    { "strIssueDate", requestData.StrIssueDate.ToString() },
                    { "additionalReferenceDesc", requestData.AdditionalReferenceDesc },
                    { "additionalReferenceDate", requestData.AdditionalReferenceDate.ToString() },
                    { "reasonDelete", requestData.ReasonDelete }
                };

                var formContent = new FormUrlEncodedContent(formData);

                // Gửi yêu cầu và nhận phản hồi
                var response = client.PostAsync(url, formContent).Result;

                // Đọc nội dung của phản hồi
                string responseContent = response.Content.ReadAsStringAsync().Result;

                // Phân tích phản hồi JSON
                dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                // Xử lý phản hồi
                if ( response.IsSuccessStatusCode)
                {
                    errorCode = responseObject.code;
                    message = responseObject.message;
                    result = true;
                }
                else
                {
                    // Xử lý phản hồi lỗi
                    errorCode = responseObject.code;
                    message = responseObject.message;
                }
            }

            return (result, errorCode , message, responseData);
        }

        /// <summary>
        /// Tra cứu dữ liệu hóa đơn phân trang
        /// </summary>
        /// <param name="url">Địa chỉ toàn vẹn API</param>
        /// <param name="userName">Mã số thuế</param>
        /// <param name="passWord">Mật khẩu</param>
        /// <param name="requestData">Request tra cứu hóa đơn</param>
        /// <returns>
        /// bool: Tích hợp thành công hay thất bại
        /// string: Mã lỗi
        /// string: Mô tả lỗi
        /// LookupInvoiceResponse: Dữ liệu hóa đơn
        /// </returns>
        public static (bool, string, string, int, List<LookupInvoiceResponse>) LookupInvoices(
            string url, string userName, string passWord, LookupInvoiceRequest requestData)
        {
            bool result = false;
            string errorCode = string.Empty, message = string.Empty;
            int totalRows = 0;
            List<LookupInvoiceResponse> responseData = new List<LookupInvoiceResponse>();

            using (var client = new HttpClient()) 
            {
                string basic = CreateBasicAuthHeader(userName, passWord);
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("Authorization", basic);

                // Serialize đối tượng thành chuỗi JSON
                string jsonData = JsonConvert.SerializeObject(requestData, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });

                // Tạo HttpRequestMessage với phương thức POST và dữ liệu JSON
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Gửi yêu cầu và nhận phản hồi
                HttpResponseMessage response = client.SendAsync(requestMessage).Result;

                // Xử lý phản hồi
                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung của phản hồi
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Phân tích phản hồi JSON
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

                    // Xử lý phản hồi thành công
                    totalRows = responseObject.totalRows;

                    responseData = JsonConvert.DeserializeObject<List<LookupInvoiceResponse>>(responseObject.invoices.ToString());

                    // Gán kết quả xử lý là true
                    result = true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Đọc nội dung của phản hồi
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Phân tích phản hồi JSON
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);


                    // Xử lý phản hồi lỗi
                    errorCode = responseObject.code;
                    message = responseObject.message;
                }
                else
                {
                    result = false;
                    errorCode = response.StatusCode.ToString();
                }
            }

            return (result, errorCode, message, totalRows, responseData);
        }



        #region support
        static string CreateBasicAuthHeader(string username, string password)
        {
            string authInfo = $"{username}:{password}";
            string encodedAuthInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            return "Basic " + encodedAuthInfo;
        }
        #endregion
    }
}
