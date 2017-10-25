using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Web.Controllers
{
    [AdminLog]
    [Authorize(Roles = "Super Admin")]
    [RoutePrefix("api/users")]
    public class UserAPIController : BaseApiController
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IUserRoleRepository _userRoleRepository;

        public UserAPIController(IErrorRepository errorRepository, IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
            : base(errorRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _userRepository.GetAll().ToViewModel();
                foreach (var i in result)
                {
                    i.Roles = string.Join(", ", _roleRepository.GetByUser(i.Id).Select(r => r.Name));
                }
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri]int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _userRepository.Get(id);
                if (result != null)
                {
                    return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel()));
                }
                return request.CreateResponse(HttpStatusCode.NotFound, new ApiResult(false, "Id not found!"));
            });
        }

        [HttpGet]
        [Route("{page}/{size}/{searchKey?}")]
        public HttpResponseMessage GetPage(HttpRequestMessage request, int page, int size, string searchKey = "")
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _userRepository.GetListPaging(out total, page, size, searchKey ?? "").ToViewModel();
                foreach (var i in result)
                {
                    i.ListRole = _roleRepository.GetAll().ToViewModel().ToList();
                    var roles = _roleRepository.GetByUser(i.Id).ToViewModel();
                    i.Roles = string.Join(", ", roles.Select(r => r.Name));
                    foreach(var role in roles)
                    {
                        i.ListRole.FirstOrDefault(r => r.Id == role.Id).Enable = true;
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var result = _userRepository.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
        }

        [HttpPost]
        [Route("updaterole")]
        public HttpResponseMessage Insert(HttpRequestMessage request, [FromBody] List<UserRole> userRoles)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _userRoleRepository.UpdateByUser(userRoles);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
            });
        }
    }
}