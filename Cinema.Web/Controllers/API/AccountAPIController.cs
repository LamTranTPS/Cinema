using Cinema.Data.Repositories;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cinema.Web.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountAPIController : BaseApiController
    {
        private ApplicationUserManager _userManager;

        public AccountAPIController(ApplicationUserManager userManager, IErrorRepository errorRepository)
            : base(errorRepository)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, [FromBody] AccountLogin account)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var result = await _userManager.FindAsync(account.UserName, account.Password);
            if (result != null)
            {
                var user = _userManager.FindAsync(account.UserName, account.Password);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel()));
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new ApiResult(false, null));
        }

        [HttpGet]
        [Authorize]
        [Route("CheckLogin")]
        public ApiResult CheckLogin()
        {
            return new ApiResult(true, "OK");
        }
    }
}