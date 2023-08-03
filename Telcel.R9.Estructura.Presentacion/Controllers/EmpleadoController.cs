using Microsoft.AspNetCore.Mvc;

namespace Telcel.R9.Estructura.Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            Negocio.Empleado empleado = new Negocio.Empleado();
            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            Negocio.Result result = Negocio.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetAll(Negocio.Empleado empleado)
        {
            Negocio.Result result = Negocio.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                empleado.Empleados = new List<object>();
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form()
        {
            Negocio.Result resultPuesto = Negocio.Puesto.GetAll();
            Negocio.Result resultDepartamento = Negocio.Departamento.GetAll();
            Negocio.Empleado empleado = new Negocio.Empleado();
            empleado.Puesto = new Negocio.Puesto();
            empleado.Departamento = new Negocio.Departamento();

            empleado.Puesto.Puestos = resultPuesto.Objects;
            empleado.Departamento.Departamentos = resultDepartamento.Objects;

            ViewBag.Titulo = "Registrar empledao";
            ViewBag.Accion = "Regristrar";
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Form(Negocio.Empleado empleado)
        {
            Negocio.Result result = Negocio.Empleado.Add(empleado);
            if (result.Correct)
            {
                ViewBag.Titulo = "¡Registro exitoso!";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                ViewBag.Titulo = "¡Error al registrar!";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(int EmpleadoID)
        {
            Negocio.Result result = Negocio.Empleado.Delete(EmpleadoID);
            if (result.Correct)
            {
                ViewBag.Titulo = "Registro eliminado";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                ViewBag.Titulo = "Error al eliminar el registro";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
        }
    }
}
