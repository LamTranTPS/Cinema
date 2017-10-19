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
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/locations")]
    public class LocationAPIController : BaseApiController
    {
        private ILocationRepository _locationRepository;

        public LocationAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, ILocationRepository locationRepository)
            :base(userManager, signInManager,errorRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCinemaChains(HttpRequestMessage request)
        {
            var result = _locationRepository.GetAll();
            return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
        }
    }
}
