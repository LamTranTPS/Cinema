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

        public QuartzScheduleAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, IQuartzScheduleRepository quartzScheduleRepository)
            : base(userManager, signInManager, errorRepository)
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
        public HttpResponseMessage GetUsers(HttpRequestMessage request, int page, int size, string searchKey = "")
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var result = _quartzScheduleRepository.GetListPaging(out total, page, size, new string[] { "Job" } , searchKey ?? "").ToViewModel();
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, total));
            });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            QuartzConfig.DeleteJob(id).Wait();
            var result = _quartzScheduleRepository.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK, new ApiResult(result, result));
        }

        [HttpPost]
        [Route("insert")]
        public HttpResponseMessage Add(HttpRequestMessage request, [FromBody] QuartzScheduleViewModel schedule)
        {
            return CreateHttpResponse(request, () =>
            {
                var result = _quartzScheduleRepository.Add(schedule.ToEntityModel());
                var newSchedule = _quartzScheduleRepository.Get(s => s.ID == result.ID, new string[] { "Job" });
                QuartzConfig.AddScheduleAsync(newSchedule).Wait();                
                return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result));
            });
        }
    }
}