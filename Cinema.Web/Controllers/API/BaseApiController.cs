using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Cinema.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IErrorRepository _errorRepository;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public BaseApiController()
        {
            
        }

        public BaseApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorRepository errorRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _errorRepository = errorRepository;
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }





        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex, requestMessage);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, new ApiResult(false, ex.Message));
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx, requestMessage);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, new ApiResult(false, dbEx.Message));
            }
            catch (Exception ex)
            {
                LogError(ex, requestMessage);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, new ApiResult(false, ex.Message));
            }
            return response;
        }

        private void LogError(Exception ex, HttpRequestMessage requestMessage)
        {
            try
            {
                Error error = new Error();
                error.Action = "API Request: " + requestMessage.Method +  requestMessage.RequestUri.LocalPath;
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorRepository.Add(error);
            }
            catch
            {
            }
        }





    }
}