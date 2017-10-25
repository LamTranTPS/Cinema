using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Web.Controllers.API
{
    [AdminLog]
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/locations")]
    public class LocationAPIController : BaseApiController
    {
        private ILocationRepository _locationRepository;

        public LocationAPIController(IErrorRepository errorRepository, ILocationRepository locationRepository)
            : base(errorRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _locationRepository.GetAll();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }
    }
}