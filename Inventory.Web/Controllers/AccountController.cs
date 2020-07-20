using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}