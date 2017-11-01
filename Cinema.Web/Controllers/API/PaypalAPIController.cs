using Cinema.Data.Repositories;
using Cinema.Web.Models;
using Cinema.Common;
using PayPal.Api;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace Cinema.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/paypals")]
    public class PaypalAPIController : BaseApiController
    {

        public PaypalAPIController(IErrorRepository errorRepository)
            : base(errorRepository)
        {

        }


        [Authorize(Roles = "Super Admin")]
        [Route("accesstoken")]
        [HttpGet]
        public HttpResponseMessage GetAccessToken(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                Dictionary<string, string> sdkConfig = new Dictionary<string, string>();
                sdkConfig.Add("mode", "sandbox");
                string accessToken = new OAuthTokenCredential(AppConstants.PayPal.ClientID, AppConstants.PayPal.Secret, sdkConfig).GetAccessToken();
                if (accessToken != null)
                {
                    return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, accessToken));
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, new ApiResult(true, accessToken));
                }

            });
        }

        [Route("payment")]
        [HttpPost]
        public HttpResponseMessage payment(HttpRequestMessage request, [FromBody]ItemViewModel item)
        {
            return CreateHttpResponse(request, () =>
            {
                APIContext apiContext = PaypalConfig.GetAPIContext();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, CreatePayment(apiContext, item)));
            });
        }

        private Payment CreatePayment(APIContext apiContext, ItemViewModel item)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = item.name,
                currency = item.currency,
                price = item.price.ToString(),
                quantity = item.quantity.ToString(),
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = item.RedirectUrl + "?status=cancel",
                return_url = item.RedirectUrl + "?status=success"
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = (item.price * item.quantity).ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (int.Parse(details.subtotal) + 2).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = "your invoice number",
                amount = amount,
                item_list = itemList
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return payment.Create(apiContext);
        }

        [Route("execute/{payerId}/{paymentId}")]
        [HttpGet]
        public HttpResponseMessage ExecutePayment(HttpRequestMessage request, string payerId, string paymentId)
        {
            return CreateHttpResponse(request, () =>
            {
                APIContext apiContext = PaypalConfig.GetAPIContext();
                var paymentExecution = new PaymentExecution() { payer_id = payerId };
                var payment = new Payment() { id = paymentId };
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, payment.Execute(apiContext, paymentExecution)));
            });
        }
    }
}