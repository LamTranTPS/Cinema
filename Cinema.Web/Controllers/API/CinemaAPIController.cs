using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using Cinema.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
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

        public CinemaAPIController(IErrorRepository errorRepository, ICinemaRepository cinemaRepository)
            : base(errorRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri]int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.Get(id);
                if (result != null)
                {
                    return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result.ToViewModel()));
                }
                return request.CreateResponse(HttpStatusCode.NotFound, new ApiResult(false, "Id not found!"));
            });
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.GetAll().ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }

        [HttpGet]
        [Route("{page}/{size}/{searchKey?}")]
        public HttpResponseMessage GetPage(HttpRequestMessage request, int page, int size, string searchKey = "")
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _cinemaRepository.GetListPaging(out total, page, size, searchKey ?? "").ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("film/{filmId}")]
        public HttpResponseMessage GetByFilm(HttpRequestMessage request, int filmId)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.GetByFilm(filmId);
                List<CinemaViewModel> listViewModel = new List<CinemaViewModel>();
                foreach(var cinema in result)
                {
                    var viewModel = cinema.ToViewModel();
                    viewModel.ScheduleCount = cinema.Schedules.Where(s => s.FilmID == filmId).Count();
                    listViewModel.Add(viewModel);
                }
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, listViewModel, listViewModel.Count));
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.Delete(id);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
            });
        }

        [HttpPost]
        [Route("insert")]
        public HttpResponseMessage Insert(HttpRequestMessage request, [FromBody] CinemaViewModel cinema)
        {
            return CreateHttpResponse(request, () =>
            {
                DateTime dt1970 = new DateTime(1970, 1, 1);
                DateTime current = DateTime.Now;
                TimeSpan span = current - dt1970;
                cinema.ID = (int)span.TotalMilliseconds;
                var result = _cinemaRepository.Add(cinema.ToEntityModel());
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, [FromBody] CinemaViewModel cinema)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _cinemaRepository.Update(cinema.ToEntityModel());
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            });
        }
    }
}