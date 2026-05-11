using Microsoft.AspNetCore.Mvc;
using HospitalVeterinario.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalVeterinario.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string password)
        {
            // ADMIN fijo
            if (usuario == "admin" && password == "123")
            {
                HttpContext.Session.SetString("Rol", "Admin");
                HttpContext.Session.SetString("Usuario", "admin");

                return RedirectToAction("Index", "Admin");
            }

            // LOGIN DESDE TABLA USERS
            var user = _context.Users
                .FirstOrDefault(u => u.Username == usuario && u.PasswordHash == password);

            if (user != null)
            {
                var rol = string.IsNullOrEmpty(user.Rol) ? "Empleado" : user.Rol;

                HttpContext.Session.SetString("Rol", rol);
                HttpContext.Session.SetString("Usuario", user.Username);

                if (user.EmpleadoId != null && user.EmpleadoId > 0)
                {
                    HttpContext.Session.SetInt32("EmpleadoId", user.EmpleadoId.Value);
                }

                if (rol == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Index", "Trabajador");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }
    }
}