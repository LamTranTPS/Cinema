using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using Cinema.Web.Models.ViewModels;
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

        public RoleAPIController(IErrorRepository errorRepository, IRoleRepository roleRepository)
            : base(errorRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _roleRepository.GetAll();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel(), result.Count()));
            });
        }

        [HttpPost]
        [Route("insert")]
        public HttpResponseMessage Insert(HttpRequestMessage request, [FromBody] RoleViewModel role)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _roleRepository.Add(role.ToEntityModel());
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel()));
            });
        }
    }
}