using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Web.Controllers.API
{
    [AdminLog]
    [Authorize(Roles = "Super Admin")]
    [RoutePrefix("api/quartzjobs")]
    public class QuartzJobAPIController : BaseApiController
    {
        private IQuartzJobRepository _quartzJobRepository;

        public QuartzJobAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, IQuartzJobRepository quartzJobRepository)
            : base(userManager, signInManager, errorRepository)
        {
            _quartzJobRepository = quartzJobRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _quartzJobRepository.GetAll().ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }
    }
}