using Cinema.Data.Repositories;
using Cinema.Web.Models;
using Microsoft.AspNet.Identity;

namespace Cinema.Web.Controllers
{
    public class MeController : BaseApiController
    {
        private ApplicationUserManager _userManager;

        public MeController(ApplicationUserManager userManager, IErrorRepository errorRepository)
            : base(errorRepository)
        {
            _userManager = userManager;
        }
        // GET api/Me
        public GetViewModel Get()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            return new GetViewModel() { Hometown = user.Email };
        }
    }
}