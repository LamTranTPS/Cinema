using Cinema.Data.Repositories;
using Cinema.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity.Owin;

namespace Cinema.Web.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountAPIController : BaseApiController
    {
        public AccountAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository)
            : base(userManager, signInManager, errorRepository)
        {

        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, [FromBody] AccountLogin account)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var result = await SignInManager.PasswordSignInAsync(account.UserName, account.Password, account.RememberMe, shouldLockout: false);
            if (result == SignInStatus.Success)
            {
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, new ApiResult(false, result, result.ToString()));
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