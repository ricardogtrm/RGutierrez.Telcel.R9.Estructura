using Microsoft.AspNetCore.Mvc;

namespace Telcel.R9.Estructura.Presentacion.Controllers
{
    public class EmpleadoCargaMasivaController : Controller
    {
        public IActionResult GetCargaMasivaTxt()
        {
            Negocio.Result result = new Negocio.Result();
            return View(result);
        }

        [HttpPost]
        public IActionResult PostCargaMasivaTxt(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return RedirectToAction("GetCargaMasivaTxt");
            }
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                List<object> registrosExitosos = new List<object>();
                List<string> registrosConError = new List<string>();
                string linea = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    linea = reader.ReadLine();
                    var valores = linea.Split('|');
                    Negocio.Empleado empleado = new Negocio.Empleado();
                    empleado.Nombre = valores[0];
                    empleado.Departamento = new Negocio.Departamento();
                    empleado.Departamento.DepartamentoID = int.Parse(valores[1]);
                    empleado.Puesto = new Negocio.Puesto();
                    empleado.Puesto.PuestoID = int.Parse(valores[2]);

                    Negocio.Result result = Negocio.Empleado.Add(empleado);
                    if (result.Correct)
                    {
                        registrosExitosos.Add(valores);
                    }
                    else
                    {
                        registrosConError.Add(linea);
                    }
                }
            }
            return Redirect("~/Empleado/GetAll");
        }
    }
}
