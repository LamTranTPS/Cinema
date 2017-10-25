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
    [RoutePrefix("api/cinemachains")]
    public class CinemaChainAPIController : BaseApiController
    {
        private ICinemaChainRepository _cinemaChainRepository;

        public CinemaChainAPIController(IErrorRepository errorRepository, ICinemaChainRepository cinemaChainRepository)
            : base(errorRepository)
        {
            _cinemaChainRepository = cinemaChainRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaChainRepository.GetAll();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }
    }
}