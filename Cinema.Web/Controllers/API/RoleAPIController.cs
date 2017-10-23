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
    [RoutePrefix("api/roles")]
    public class RoleAPIController : BaseApiController
    {
        private IRoleRepository _roleRepository;

        public RoleAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, IRoleRepository roleRepository)
            : base(userManager, signInManager, errorRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _roleRepository.GetAll();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel(), result.Count()));
            });
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Insert(HttpRequestMessage request, [FromBody] Role role)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _roleRepository.Add(role);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel()));
            });
        }
    }
}