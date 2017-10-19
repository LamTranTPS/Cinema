using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Web.Controllers.API
{
    [AdminLog]
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/cinemas")]
    public class CinemaAPIController : BaseApiController
    {
        private ICinemaRepository _cinemaRepository;

        public CinemaAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, ICinemaRepository cinemaRepository)
            : base(userManager, signInManager, errorRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCinemas(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.GetAll().ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }

        [HttpGet]
        [Route("{page}/{size}/{searchKey?}")]
        public HttpResponseMessage GetUsers(HttpRequestMessage request, int page, int size, string searchKey = "")
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _cinemaRepository.GetListPaging(out total, page, size, searchKey ?? "").ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.Delete(id);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
            });
        }

        [HttpPost]
        [Route("insert")]
        public HttpResponseMessage Add(HttpRequestMessage request, [FromBody] Model.Models.Cinema cinema)
        {
            return CreateHttpResponse(request, () =>
            {
                DateTime dt1970 = new DateTime(1970, 1, 1);
                DateTime current = DateTime.Now;
                TimeSpan span = current - dt1970;
                cinema.ID = span.TotalMilliseconds.ToString();
                var result = _cinemaRepository.Add(cinema);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            });
        }
    }
}