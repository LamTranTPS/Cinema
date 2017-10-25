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
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/schedules")]
    public class ScheduleAPIController : BaseApiController
    {
        private IScheduleRepository _scheduleRepository;

        public ScheduleAPIController(IErrorRepository errorRepository, IScheduleRepository scheduleRepository)
            : base(errorRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _scheduleRepository.GetAll().ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }

        [HttpGet]
        [Route("{cinemaId}/{filmId}/{page}/{size}")]
        public HttpResponseMessage GetPageByCinemaAndFilm(HttpRequestMessage request, int cinemaId, int filmId, int page, int size)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _scheduleRepository.GetByCinemaAndFilm(cinemaId, filmId, out total, page, size).ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("cinema/{cinemaId}/{page}/{size}")]
        public HttpResponseMessage GetPageByCinema(HttpRequestMessage request, int cinemaId, int page, int size)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _scheduleRepository.GetByCinema(cinemaId, out total, page, size).ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("film/{filmId}/{page}/{size}")]
        public HttpResponseMessage GetPageByFilm(HttpRequestMessage request, int filmId, int page, int size)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _scheduleRepository.GetByFilm(filmId, out total, page, size).ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _scheduleRepository.Delete(id);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
            });
        }

        [HttpPost]
        [Route("insert")]
        public HttpResponseMessage Insert(HttpRequestMessage request, [FromBody] ScheduleViewModel schedule)
        {
            return CreateHttpResponse(request, () =>
            {
                schedule.ID = schedule.FilmID + "/" + schedule.CinemaID + "/" + schedule.DateTime.ToString("yyyy-MM-dd#HH:mm");
                var result = _scheduleRepository.Add(schedule.ToEntityModel());
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            });
        }
    }
}