using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Cinema.Web.Models.Extensions;
using Cinema.Web.Models.ViewModels;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Web.Controllers
{
    [AdminLog]
    [Authorize(Roles = "Super Admin")]
    [RoutePrefix("api/quartzschedules")]
    public class QuartzScheduleAPIController : BaseApiController
    {
        private IQuartzScheduleRepository _quartzScheduleRepository;

        public QuartzScheduleAPIController(IErrorRepository errorRepository, IQuartzScheduleRepository quartzScheduleRepository)
            : base(errorRepository)
        {
            _quartzScheduleRepository = quartzScheduleRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _quartzScheduleRepository.GetAll().ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
            });
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri]int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _quartzScheduleRepository.Get(id);
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
                var result = _quartzScheduleRepository.GetListPaging(out total, page, size, searchKey ?? "").ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                QuartzConfig.DeleteJob(id).Wait();
                var result = _quartzScheduleRepository.Delete(id);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
            });
        }

        [HttpPost]
        [Route("insert")]
        public HttpResponseMessage Insert(HttpRequestMessage request, [FromBody] QuartzScheduleViewModel schedule)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _quartzScheduleRepository.Add(schedule.ToEntityModel());
                var newSchedule = _quartzScheduleRepository.Get(s => s.ID == result.ID);
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            });
        }

        [HttpGet]
        [Route("start/{id}")]
        public HttpResponseMessage Start(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                _quartzScheduleRepository.UpdateStatus(id, true);
                QuartzConfig.StartJob(id).Wait();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, true));
            });
        }

        [HttpGet]
        [Route("pause/{id}")]
        public HttpResponseMessage Pause(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                _quartzScheduleRepository.UpdateStatus(id, false);
                QuartzConfig.PauseJob(id).Wait();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, true));
            });
        }
    }
}