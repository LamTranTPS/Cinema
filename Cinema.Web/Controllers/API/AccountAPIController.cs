using Cinema.Data.Repositories;
using Cinema.Web.Models;
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
        private ApplicationSignInManager _signInManager;

        public AccountAPIController(ApplicationSignInManager signInManager, IErrorRepository errorRepository)
            : base(errorRepository)
        {
            _signInManager = signInManager;
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
            var result = await _signInManager.PasswordSignInAsync(account.UserName, account.Password, account.RememberMe, shouldLockout: false);
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