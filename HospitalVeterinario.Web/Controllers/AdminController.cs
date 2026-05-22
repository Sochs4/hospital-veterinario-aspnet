using HospitalVeterinario.Data;
using HospitalVeterinario.Data.Models;
using HospitalVeterinario.Services; // Asegúrate de tener este using
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HospitalVeterinario.Web.Controllers
{




    public class AdminController : Controller
    {

        private readonly IEmpleadoService _empleadoService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IPuestoService _puestoService;
        private readonly IUserService _usuarioService = null!;

        private readonly INominaService _nominaService;
        private readonly ApplicationDbContext _context;


        // Constructor que recibe las interfaces de servicio por inyección de dependencias



        public IActionResult SolicitudesVacaciones()
        {
            var solicitudes = _context.Vacacion
                .Include(v => v.Empleado)
                .OrderByDescending(v => v.FechaSolicitud)
                .ToList();

            return View(solicitudes);
        }

        [HttpPost]
        public IActionResult RecontratarEmpleado(int id)
        {
            var empleado = _context.Empleados.Find(id);

            if (empleado == null)
                return NotFound();

            empleado.Activo = true;

            _context.SaveChanges();

            TempData["Mensaje"] = "Empleado recontratado correctamente.";

            return RedirectToAction("DespedirEmpleado");
        }


        public IActionResult DespedirEmpleado()
        {
            var empleados = _context.Empleados.ToList();
            return View(empleados);
        }
        [HttpPost]
        public IActionResult ProcesarDespido(int empleadoId)
        {
            var empleado = _context.Empleados.FirstOrDefault(e => e.IdEmpleado == empleadoId);

            if (empleado == null)
                return NotFound();

            DateTime fechaIngreso = empleado.FechaContratacion ?? DateTime.Now;
            DateTime fechaSalida = DateTime.Now;

            int diasTrabajados = (fechaSalida - fechaIngreso).Days;
            decimal aniosTrabajados = diasTrabajados / 365m;

            decimal salarioBase = empleado.SalarioBase ?? 0;
            decimal indemnizacion = salarioBase * aniosTrabajados;

            var pagoLiquidacion = new Nomina
            {
                EmpleadoId = empleado.IdEmpleado,
                TipoPago = "Liquidación",
                FechaPago = fechaSalida,
                Mes = fechaSalida.Month,
                Anio = fechaSalida.Year,
                SalarioBase = salarioBase,
                SalarioBruto = indemnizacion,
                SalarioNeto = indemnizacion,
                DiasHabilesMes = 0,
                DiasTrabajados = 0,
                DiasAusentes = 0,
                DescuentoIGSS = 0,
                DescuentoISR = 0,
                DescuentoAusencias = 0,
                DescuentoSeptimo = 0,
                TotalDescuentos = 0,
                Bono14 = 0,
                Aguinaldo = 0,
                PorcentajeProductividad = 0,
                NivelProductividad = "Liquidación"
            };

            _context.Nominas.Add(pagoLiquidacion);
            _context.SaveChanges();

            TempData["Mensaje"] = "Empleado despedido y liquidación enviada al flujo de pagos.";

            return RedirectToAction("FlujoPagos");
        }
        public IActionResult ConfirmarDespido(int id)
        {
            var empleado = _context.Empleados.FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound();

            DateTime fechaIngreso = empleado.FechaContratacion ?? DateTime.Now;
            DateTime fechaSalida = DateTime.Now;

            int diasTrabajados = (fechaSalida - fechaIngreso).Days;
            decimal aniosTrabajados = diasTrabajados / 365m;

            decimal salarioBase = empleado.SalarioBase ?? 0;
            decimal indemnizacion = (salarioBase / 365m) * diasTrabajados;

            ViewBag.FechaSalida = fechaSalida.ToString("dd/MM/yyyy");
            ViewBag.DiasTrabajados = diasTrabajados;
            ViewBag.AniosTrabajados = aniosTrabajados.ToString("0.00");
            ViewBag.Indemnizacion = indemnizacion;

            return View(empleado);
        }


        public IActionResult ProductividadEmpleado(int? empleadoId, int? mes, int? anio, string? tipoPago)
        {
            ViewBag.Empleados = _context.Empleados.ToList();
            ViewBag.EmpleadoId = empleadoId;
            ViewBag.Mes = mes;
            ViewBag.Anio = anio;
            ViewBag.TipoPago = tipoPago;

            var consulta = _context.Nominas.AsQueryable();

            if (empleadoId.HasValue)
                consulta = consulta.Where(n => n.EmpleadoId == empleadoId.Value);

            if (mes.HasValue)
                consulta = consulta.Where(n => n.Mes == mes.Value);

            if (anio.HasValue)
                consulta = consulta.Where(n => n.Anio == anio.Value);

            if (!string.IsNullOrEmpty(tipoPago))
                consulta = consulta.Where(n => n.TipoPago == tipoPago);

            var datos = consulta
                .OrderBy(n => n.FechaPago)
                .ToList();

            foreach (var item in datos)
            {
                item.Empleado = _context.Empleados
                    .FirstOrDefault(e => e.IdEmpleado == item.EmpleadoId);
            }

            return View(datos);
        }

        public IActionResult FlujoPagos(string? nombre, int? anio)
        {
            var pagos = _context.Nominas
                .OrderByDescending(n => n.FechaPago)
                .ToList();

            foreach (var pago in pagos)
            {
                pago.Empleado = _context.Empleados
                    .FirstOrDefault(e => e.IdEmpleado == pago.EmpleadoId);
            }

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                pagos = pagos
                    .Where(p =>
                        ((p.Empleado?.Nombre ?? "") + " " + (p.Empleado?.Apellido ?? ""))
                        .ToLower()
                        .Contains(nombre.ToLower()))
                    .ToList();
            }

            if (anio.HasValue)
            {
                pagos = pagos
                    .Where(p => p.Anio == anio.Value)
                    .ToList();
            }

            ViewBag.Nombre = nombre;
            ViewBag.Anio = anio;

            return View(pagos);
        }
        [HttpPost]
        public IActionResult EliminarPago(int id)
        {
            var pago = _context.Nominas.Find(id);

            if (pago == null)
            {
                TempData["Error"] = $"No se encontró el pago con ID {id}.";
                return RedirectToAction("FlujoPagos");
            }

            _context.Nominas.Remove(pago);
            _context.SaveChanges();

            TempData["Mensaje"] = "Pago eliminado correctamente.";

            return RedirectToAction("FlujoPagos");
        }

        public IActionResult SeleccionarEmpleadoPago()
        {
            var empleados = _empleadoService.GetAllEmpleados();
            return View(empleados);
        }

        public IActionResult RealizarPago(int id)
        {
            var empleado = _context.Empleados.Find(id);

            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        private int CalcularDiasHabilesDelMes(int mes, int anio)
        {
            int diasDelMes = DateTime.DaysInMonth(anio, mes);
            int diasHabiles = 0;

            for (int dia = 1; dia <= diasDelMes; dia++)
            {
                DateTime fecha = new DateTime(anio, mes, dia);

                if (fecha.DayOfWeek != DayOfWeek.Saturday &&
                    fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasHabiles++;
                }
            }

            return diasHabiles;
        }

        private bool PagoAntesDeContratacion(DateTime? fechaContratacion, int mes, int anio)
        {
            if (fechaContratacion == null)
                return false;

            DateTime fechaPago = new DateTime(anio, mes, 1);
            DateTime inicioContrato = new DateTime(
                fechaContratacion.Value.Year,
                fechaContratacion.Value.Month,
                1
            );

            return fechaPago < inicioContrato;
        }


        [HttpPost]
        public IActionResult AprobarVacacion(int id)
        {
            var solicitud = _context.Vacacion.FirstOrDefault(v => v.Id == id);

            if (solicitud == null)
            {
                TempData["Error"] = "No se encontró la solicitud.";
                return RedirectToAction("SolicitudesVacaciones");
            }

            solicitud.Estado = "Aprobada";
            _context.SaveChanges();

            TempData["Mensaje"] = "Vacaciones aprobadas correctamente.";

            return RedirectToAction("SolicitudesVacaciones");
        }

        [HttpPost]
        public IActionResult RechazarVacacion(int id)
        {
            var solicitud = _context.Vacacion.FirstOrDefault(v => v.Id == id);

            if (solicitud == null)
            {
                TempData["Error"] = "No se encontró la solicitud.";
                return RedirectToAction("SolicitudesVacaciones");
            }

            solicitud.Estado = "Rechazada";
            _context.SaveChanges();

            TempData["Mensaje"] = "Solicitud rechazada correctamente.";

            return RedirectToAction("SolicitudesVacaciones");
        }
        [HttpPost]
        public IActionResult RealizarPago(int empleadoId, string tipoPago, int diasHabilesMes, int diasTrabajados, int mes, int anio, decimal horasExtras)
        {
            var empleado = _context.Empleados.Find(empleadoId);
            var pagosDelMes = _context.Nominas
                .Where(n => n.EmpleadoId == empleadoId &&
                            n.Mes == mes &&
                            n.Anio == anio)
                .ToList();

            if (pagosDelMes.Any())
            {
                string tipoPagoYaUsado = pagosDelMes.First().TipoPago;

                if (tipoPagoYaUsado != tipoPago)
                {
                    TempData["Error"] = $"Este mes ya fue iniciado con pago {tipoPagoYaUsado}. Solo se permite seguir pagando con ese mismo tipo.";
                    return RedirectToAction("RealizarPago", new { id = empleadoId });
                }

                if (tipoPago == "Mensual" && pagosDelMes.Count >= 1)
                {
                    TempData["Error"] = "Este empleado ya recibió pago mensual en este mes.";
                    return RedirectToAction("RealizarPago", new { id = empleadoId });
                }

                if (tipoPago == "Quincenal" && pagosDelMes.Count >= 2)
                {
                    TempData["Error"] = "Este empleado ya recibió los 2 pagos quincenales de este mes.";
                    return RedirectToAction("RealizarPago", new { id = empleadoId });
                }

                if (tipoPago == "Semanal" && pagosDelMes.Count >= 4)
                {
                    TempData["Error"] = "Este empleado ya recibió los 4 pagos semanales de este mes.";
                    return RedirectToAction("RealizarPago", new { id = empleadoId });
                }
            }

            if (PagoAntesDeContratacion(empleado.FechaContratacion, mes, anio))
            {
                TempData["Error"] = "No se puede registrar un pago antes de la fecha de contratación.";
                return RedirectToAction("RealizarPago", new { id = empleadoId });
            }

            
              

            int diasHabilesTotalesMes = CalcularDiasHabilesDelMes(mes, anio);

            if (tipoPago == "Mensual")
            {
                diasHabilesMes = diasHabilesTotalesMes;
            }
            else if (tipoPago == "Quincenal")
            {
                diasHabilesMes = (int)Math.Ceiling(diasHabilesTotalesMes / 2.0);
            }
            else if (tipoPago == "Semanal")
            {
                diasHabilesMes = (int)Math.Ceiling(diasHabilesTotalesMes / 4.0);
            }

            if (diasTrabajados > diasHabilesMes)
            {
                TempData["Error"] = "Los días trabajados no pueden ser mayores a los días hábiles del período.";
                return RedirectToAction("RealizarPago", new { id = empleadoId });
            }

            decimal salarioBase = empleado.SalarioBase ?? 0;
            int diasAusentes = diasHabilesMes - diasTrabajados;

            if (diasTrabajados <= 0)
            {
                var nominaSinPago = new Nomina
                {
                    EmpleadoId = empleadoId,
                    TipoPago = tipoPago,
                    Mes = mes,
                    Anio = anio,
                    FechaPago = DateTime.Now,
                    SalarioBase = salarioBase,
                    SalarioBruto = 0,
                    DiasHabilesMes = diasHabilesMes,
                    DiasTrabajados = 0,
                    DiasAusentes = diasHabilesMes,
                    DescuentoAusencias = salarioBase,
                    DescuentoSeptimo = 0,
                    DescuentoIGSS = 0,
                    DescuentoISR = 0,
                    TotalDescuentos = salarioBase,
                    SalarioNeto = 0,
                    Bono14 = 0,
                    Aguinaldo = 0,
                    PorcentajeProductividad = 0,
                    NivelProductividad = "Sin pago"
                };

                _context.Nominas.Add(nominaSinPago);
                _context.SaveChanges();

                return RedirectToAction("FlujoPagos");
            }

            decimal salarioPeriodo = salarioBase;

            if (tipoPago == "Quincenal")
            {
                salarioPeriodo = salarioBase / 2;
            }
            else if (tipoPago == "Semanal")
            {
                salarioPeriodo = salarioBase / 4;
            }

            decimal salarioBruto = (salarioPeriodo / diasHabilesMes) * diasTrabajados;
            decimal valorDia = salarioPeriodo / diasHabilesMes;
            decimal valorHora = valorDia / 8;
            decimal valorHoraExtra = valorHora * 1.5m;
            decimal montoHorasExtras = horasExtras * valorHoraExtra;

            salarioBruto += montoHorasExtras;
            decimal bono14 = 0;
            decimal aguinaldo = 0;

            if (mes == 7)
            {
                var pagosEneroJunio = _context.Nominas
                    .Where(n => n.EmpleadoId == empleadoId &&
                                n.Anio == anio &&
                                n.Mes >= 1 &&
                                n.Mes <= 6)
                    .ToList();

                if (pagosEneroJunio.Any())
                {
                    bono14 = pagosEneroJunio.Average(n => n.SalarioNeto);
                }
            }

            if (mes == 12)
            {
                var pagosJulioNoviembre = _context.Nominas
                    .Where(n => n.EmpleadoId == empleadoId &&
                                n.Anio == anio &&
                                n.Mes >= 7 &&
                                n.Mes <= 11)
                    .ToList();

                if (pagosJulioNoviembre.Any())
                {
                    aguinaldo = pagosJulioNoviembre.Average(n => n.SalarioNeto);
                }
            }


            salarioBruto += bono14 + aguinaldo;

            decimal igss = 0;
            decimal isr = 0;

            var pagosDelMesActual  = _context.Nominas
    .Where(n => n.EmpleadoId == empleadoId &&
                n.Mes == mes &&
                n.Anio == anio)
    .ToList();

            bool esPrimerPagoDelMes = !pagosDelMes.Any();

            if (esPrimerPagoDelMes)
            {
                igss = _nominaService.CalcularIGSS(salarioBase);
                isr = _nominaService.CalcularISR(salarioBase);
            }
            decimal descuentoAusencias = (salarioPeriodo / diasHabilesMes) * diasAusentes;
            decimal descuentoSeptimo = diasAusentes > 0 ? salarioBase / 30 : 0;

            decimal totalDescuentos = igss + isr;
            decimal salarioNeto = salarioBruto - totalDescuentos;

            decimal productividad = ((decimal)diasTrabajados / diasHabilesMes) * 100;

            string nivelProductividad = "Baja";
            if (productividad >= 95) nivelProductividad = "Alta";
            else if (productividad >= 80) nivelProductividad = "Media";

            var nomina = new Nomina
            {
                EmpleadoId = empleadoId,
                TipoPago = tipoPago,
                Mes = mes,
                Anio = anio,
                FechaPago = DateTime.Now,
                SalarioBase = salarioBase,
                SalarioBruto = salarioBruto,
                HorasExtras = horasExtras,
                MontoHorasExtras = montoHorasExtras,
                DiasHabilesMes = diasHabilesMes,
                DiasTrabajados = diasTrabajados,
                DiasAusentes = diasAusentes,
                DescuentoAusencias = descuentoAusencias,
                DescuentoSeptimo = descuentoSeptimo,
                DescuentoIGSS = igss,
                DescuentoISR = isr,
                TotalDescuentos = totalDescuentos,
                SalarioNeto = salarioNeto,
                Bono14 = bono14,
                Aguinaldo = aguinaldo,
                PorcentajeProductividad = productividad,
                NivelProductividad = nivelProductividad
            };

            _context.Nominas.Add(nomina);
            _context.SaveChanges();

            return RedirectToAction("FlujoPagos");
        }

        public AdminController(
            IEmpleadoService empleadoService,
            IDepartamentoService departamentoService,
            IPuestoService puestoService,
            IUserService usuarioService,
            INominaService nominaService,
            ApplicationDbContext context)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _puestoService = puestoService;
            _usuarioService = usuarioService;
            _nominaService = nominaService;
            _context = context;
        }
        public IActionResult Usuarios()
        {
            var empleados = _empleadoService.GetAllEmpleados();
            return View(empleados);
        }

        // Acción por defecto (se llama si no se especifica ninguna acción en la URL)
        public IActionResult Index()
        {
            return View(); // Asumimos que tienes una vista llamada Index.cshtml en Views/Admin/
        }

        // Ejemplo de otra acción para listar empleados
        public IActionResult ListaEmpleados()
        {
            var empleados = _empleadoService.GetAllEmpleados();
            return View(empleados); // Asumimos que tienes una vista llamada ListaEmpleados.cshtml en Views/Admin/
        }

        // Ejemplo de acción para mostrar un formulario
        public IActionResult AgregarEmpleado()
        {
            ViewBag.Departamentos = _departamentoService.GetAllDepartamentos();
            ViewBag.Puestos = _puestoService.GetAllPuestos();

            return View();
        }

        // Ejemplo de acción para recibir datos de un formulario (POST)
        [HttpPost]
        public IActionResult AgregarEmpleado(Empleado nuevoEmpleado)
        {
            // 🧪 PRUEBA
            Console.WriteLine("Nombre: " + nuevoEmpleado.Nombre);
            Console.WriteLine("Apellido: " + nuevoEmpleado.Apellido);

            if (ModelState.IsValid)
            {
                nuevoEmpleado.BonificacionDecreto = 250;

                _empleadoService.AddEmpleado(nuevoEmpleado);

                return RedirectToAction("ListaEmpleados");
            }

            ViewBag.Departamentos = _departamentoService.GetAllDepartamentos();
            ViewBag.Puestos = _puestoService.GetAllPuestos();

            return View(nuevoEmpleado);
        }
        // Agrega aquí más acciones para la funcionalidad de administración
        // (editar, eliminar empleados, crear usuarios, etc.)


        public IActionResult DetallesEmpleado(int id)
        {
            var emp = _empleadoService.GetEmpleadoById(id);

            if (emp == null)
            {
                return NotFound();
            }

            var departamento = _departamentoService.GetAllDepartamentos()
                .FirstOrDefault(d => d.IdDepartamento == emp.IdDepartamento);

            var puesto = _puestoService.GetAllPuestos()
                .FirstOrDefault(p => p.IdPuesto == emp.IdPuesto);

            ViewBag.NombreDepartamento = departamento?.NombreDepartamento ?? "No asignado";
            ViewBag.NombrePuesto = puesto?.NombrePuesto ?? "No asignado";

            return View(emp);
        }


        public IActionResult CrearUsuarioEmpleado(int id)
        {
            var empleado = _empleadoService.GetEmpleadoById(id);

            if (empleado == null)
                return NotFound();

            var usuarioExistente = _usuarioService.GetByEmpleadoId(id);

            if (usuarioExistente != null)
            {
                TempData["Error"] = "Este empleado ya tiene usuario.";
                return RedirectToAction("Usuarios");
            }

            string primerNombre = (empleado.Nombre ?? "empleado").Split(' ')[0].ToLower();

            string inicialApellido = string.IsNullOrWhiteSpace(empleado.Apellido)
                ? "x"
                : empleado.Apellido.Substring(0, 1).ToLower();

            string usuarioGenerado = primerNombre + inicialApellido + empleado.IdEmpleado;

            ViewBag.IdEmpleado = empleado.IdEmpleado;
            ViewBag.UsuarioGenerado = usuarioGenerado;
            ViewBag.NombreEmpleado = empleado.Nombre + " " + empleado.Apellido;

            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuarioEmpleado(int idEmpleado, string username, string password)
        {
            var usuarioExistente = _usuarioService.GetByEmpleadoId(idEmpleado);

            if (usuarioExistente != null)
            {
                TempData["Error"] = "Este empleado ya tiene usuario creado.";
                return RedirectToAction("Usuarios");
            }

            var user = new User
            {
                EmpleadoId = idEmpleado,
                Username = username,
                PasswordHash = password
            };

            _usuarioService.Add(user);

            TempData["Mensaje"] = "Usuario creado correctamente: " + username;

            return RedirectToAction("Usuarios");
        }
        public IActionResult EditarEmpleado(int id)
        {
            var emp = _empleadoService.GetEmpleadoById(id);

            if (emp == null)
            {
                return NotFound();
            }

            ViewBag.Departamentos = _departamentoService.GetAllDepartamentos();
            ViewBag.Puestos = _puestoService.GetAllPuestos();

            return View(emp);
        }

        public IActionResult EliminarEmpleado(int id)
        {
            _empleadoService.DeleteEmpleado(id);
            return RedirectToAction("ListaEmpleados");
        }







    }



}