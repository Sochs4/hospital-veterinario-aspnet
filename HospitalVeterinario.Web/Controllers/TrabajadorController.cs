using HospitalVeterinario.Data;
using HospitalVeterinario.Data.Models;
using HospitalVeterinario.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalVeterinario.Web.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IPuestoService _puestoService;
        private readonly ApplicationDbContext _context;

        public TrabajadorController(
            IEmpleadoService empleadoService,
            IDepartamentoService departamentoService,
            IPuestoService puestoService,
            ApplicationDbContext context)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _puestoService = puestoService;
            _context = context;
        }

        // Dashboard principal del trabajador

        public IActionResult HistorialVacaciones()
        {
            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
                return RedirectToAction("Index", "Login");

            var historial = _context.Vacacion
                .Where(v => v.EmpleadoId == empleadoId.Value)
                .OrderByDescending(v => v.FechaSolicitud)
                .ToList();

            return View(historial);
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Empleado")
            {
                return RedirectToAction("Index", "Login");
            }

            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            var empleado = _empleadoService.GetEmpleadoById(empleadoId.Value);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // Perfil del trabajador
        public IActionResult Perfil()
        {
            if (HttpContext.Session.GetString("Rol") != "Empleado")
            {
                return RedirectToAction("Index", "Login");
            }

            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            var empleado = _empleadoService.GetEmpleadoById(empleadoId.Value);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // Ver sus pagos/sueldos
        public IActionResult MisPagos()
        {
            if (HttpContext.Session.GetString("Rol") != "Empleado")
            {
                return RedirectToAction("Index", "Login");
            }

            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            var pagos = _context.Nominas
                .Where(n => n.EmpleadoId == empleadoId.Value)
                .OrderByDescending(n => n.FechaPago)
                .ToList();

            return View(pagos);
        }

        // Ver productividad
        public IActionResult MiProductividad()
        {
            if (HttpContext.Session.GetString("Rol") != "Empleado")
            {
                return RedirectToAction("Index", "Login");
            }

            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            var productividad = _context.Nominas
                .Where(n => n.EmpleadoId == empleadoId.Value)
                .OrderByDescending(n => n.FechaPago)
                .ToList();

            return View(productividad);
        }
        [HttpPost]
        public IActionResult SolicitarVacaciones(DateTime FechaInicio, DateTime FechaFin, string Motivo)
        {
            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
                return RedirectToAction("Index", "Login");

            int diasSolicitados = CalcularDiasHabiles(FechaInicio, FechaFin);

            var solicitud = new Vacacion
            {
                EmpleadoId = empleadoId.Value,
                FechaInicio = FechaInicio,
                FechaFin = FechaFin,
                DiasSolicitados = diasSolicitados,
                Motivo = Motivo ?? "",
                Estado = "Pendiente",
                FechaSolicitud = DateTime.Now
            };

            _context.Vacacion.Add(solicitud);
            _context.SaveChanges();

            TempData["Mensaje"] = "Solicitud enviada correctamente.";

            return RedirectToAction("SolicitarVacaciones");
        }

        private int CalcularDiasHabiles(DateTime inicio, DateTime fin)
        {
            int dias = 0;

            for (DateTime fecha = inicio; fecha <= fin; fecha = fecha.AddDays(1))
            {
                if (fecha.DayOfWeek != DayOfWeek.Saturday &&
                    fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    dias++;
                }
            }

            return dias;
        }
        // Solicitar vacaciones
        [HttpGet]
        public IActionResult SolicitarVacaciones()
        {
            if (HttpContext.Session.GetString("Rol") != "Empleado")
                return RedirectToAction("Index", "Login");

            var empleadoId = HttpContext.Session.GetInt32("EmpleadoId");

            if (empleadoId == null || empleadoId == 0)
                return RedirectToAction("Index", "Login");

            var empleado = _empleadoService.GetEmpleadoById(empleadoId.Value);

            if (empleado == null)
                return NotFound();

            DateTime fechaActual = DateTime.Now;
            DateTime fechaContratacion = empleado.FechaContratacion ?? fechaActual;

            int diasTrabajados = (fechaActual - fechaContratacion).Days;
            int añosCumplidos = diasTrabajados / 365;

            int diasVacacionesPorAnio = 15;
            int diasGanados = añosCumplidos * diasVacacionesPorAnio;

            int diasUsados = _context.Vacacion
                .Where(v => v.EmpleadoId == empleadoId.Value && v.Estado == "Aprobada")
                .Sum(v => v.DiasSolicitados);

            int diasPendientes = diasGanados - diasUsados;

            if (diasPendientes < 0)
                diasPendientes = 0;

            int periodosPendientes = diasPendientes / 15;

            ViewBag.FechaActual = fechaActual.ToString("dd/MM/yyyy");
            ViewBag.FechaContratacion = fechaContratacion.ToString("dd/MM/yyyy");
            ViewBag.AniosCumplidos = añosCumplidos;
            ViewBag.DiasVacacionesGanados = diasGanados;
            ViewBag.DiasUsados = diasUsados;
            ViewBag.DiasVacacionesPendientes = diasPendientes;
            ViewBag.PeriodosPendientes = periodosPendientes;

            if (periodosPendientes >= 1)
            {
                ViewBag.MensajePeriodo = $"Tiene {periodosPendientes} período(s) completo(s) pendiente(s).";
            }
            else if (diasPendientes > 0)
            {
                ViewBag.MensajePeriodo = $"Solo tiene {diasPendientes} día(s) pendiente(s), no tiene un período completo de 15 días.";
            }
            else
            {
                ViewBag.MensajePeriodo = "No tiene vacaciones pendientes.";
            }

            return View();
        }

        public IActionResult ListaDepartamentos()
        {
            var departamentos = _departamentoService.GetAllDepartamentos();
            return View(departamentos);
        }

        public IActionResult ListaPuestos()
        {
            var puestos = _puestoService.GetAllPuestos();
            return View(puestos);
        }

        public IActionResult PruebaSimple()
        {
            ViewBag.Mensaje = "La acción PruebaSimple se ejecutó!";
            return View();
        }
    }
}