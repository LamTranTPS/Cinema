using Cinema.Web.Models;
using Microsoft.AspNet.Identity;

namespace Cinema.Web.Controllers
{
    public class MeController : BaseApiController
    {
        // GET api/Me
        public GetViewModel Get()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<int>());
            return new GetViewModel() { Hometown = user.Email };
        }
    }
}