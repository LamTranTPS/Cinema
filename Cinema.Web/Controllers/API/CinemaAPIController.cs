using Cinema.Data;
using Cinema.Data.Repositories;
using Cinema.Web.ActionFilters;
using Cinema.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Cinema.Web.Controllers.API
{
    [AdminLog]
    [Authorize(Roles="admin")]
    [RoutePrefix("api/cinemas")]
    public class CinemaAPIController : BaseApiController
    {
        private ICinemaRepository _cinemaRepository;

        public CinemaAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, ICinemaRepository cinemaRepository)
            :base(userManager, signInManager,errorRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCinemas(HttpRequestMessage request)
        {
            var result = _cinemaRepository.GetAll();
            return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
        }
    }
}
