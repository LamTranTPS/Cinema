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
    [RoutePrefix("api/cinemachains")]
    public class CinemaChainAPIController : BaseApiController
    {
        private ICinemaChainRepository _cinemaChainRepository;

        public CinemaChainAPIController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository, ICinemaChainRepository cinemaChainRepository)
            :base(userManager, signInManager,errorRepository)
        {
            _cinemaChainRepository = cinemaChainRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCinemaChains(HttpRequestMessage request)
        {
            var result = _cinemaChainRepository.GetAll();
            return request.CreateResponse(HttpStatusCode.OK, new ApiResult(true, result, result.Count()));
        }
    }
}
