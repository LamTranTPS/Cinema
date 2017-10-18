using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Web.Controllers
{
    [AdminLog]
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/users")]
    public class UserAPIController : BaseApiController
    {

        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public UserAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, IUserRepository userRepository, IRoleRepository roleRepository)
            : base(userManager, signInManager, errorRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetUsers(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = UserManager.Users.ToList().ToViewModel();
                foreach(var i in result)
                {
                    i.Roles = string.Join(", ", _roleRepository.GetByUser(i.Id).Select(r => r.Name));
                }
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetUser(HttpRequestMessage request, [FromUri]string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = UserManager.FindByIdAsync(id).Result;

                if (result != null)
                {
                    return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel()));
                }
                return request.CreateResponse(HttpStatusCode.NotFound, new ApiResult(false, "Id not found!"));
            });
        }

        [HttpGet]
        [Route("{page}/{size}")]
        public HttpResponseMessage GetUsers(HttpRequestMessage request, int page, int size)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _userRepository.GetListPaging(out total, page, size, "").ToViewModel();
                foreach (var i in result)
                {
                    i.Roles = string.Join(", ", _roleRepository.GetByUser(i.Id).Select(r => r.Name));
                }
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("{page}/{size}/{searchKey}")]
        public HttpResponseMessage GetUsers(HttpRequestMessage request, int page, int size, string searchKey)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _userRepository.GetListPaging(out total, page, size, searchKey ?? "").ToViewModel();
                foreach (var i in result)
                {
                    i.Roles = string.Join(", ", _roleRepository.GetByUser(i.Id).Select(r => r.Name));
                }
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }
    }
}