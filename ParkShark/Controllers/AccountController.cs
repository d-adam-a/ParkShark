using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ParkShark.Data;
using ParkShark.Models.ViewModels;
using System.Security.Claims;

namespace ParkShark.Controllers
{
    public class AccountController : Controller
    {
        private readonly MysqlContext _context;
        public AccountController(MysqlContext c)
        {
            _context = c;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login login)
        {
            var user = _context.Users
                .Where(x => x.Username == login.Username && x.Password == login.Password)
                .FirstOrDefault();

            if(user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim("username", user.Username),
                    new Claim("name", user.Fullname),
                    new Claim("role", "User")
                };

                var identity = new ClaimsIdentity(claims, "Cookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return Redirect("/Parking/Index");
            }

            return View();
        }
    }
}
