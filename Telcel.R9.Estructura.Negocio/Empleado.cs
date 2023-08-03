using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public Puesto Puesto { get; set; }
        public Departamento Departamento { get; set; }
        public List<object> Empleados { get; set; }

        public static Result GetAll(Empleado empleadoHead)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.RgutierrezEstructuraContext context = new AccesoDatos.RgutierrezEstructuraContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleadoHead.Nombre}'").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            Empleado empleado = new Empleado();
                            empleado.EmpleadoID = item.EmpleadoId;
                            empleado.Nombre = item.Nombre;
                            empleado.Puesto = new Puesto();
                            empleado.Puesto.PuestoID = item.PuestoId.Value;
                            empleado.Puesto.Descripcion = item.DescripcionPuesto;
                            empleado.Departamento = new Departamento();
                            empleado.Departamento.DepartamentoID = item.DepartamentoId.Value;
                            empleado.Departamento.Descripcion = item.DescripcionDepartamento;

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static Result Add(Empleado empleado)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.RgutierrezEstructuraContext context = new AccesoDatos.RgutierrezEstructuraContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}', {empleado.Puesto.PuestoID}" +
                        $",{empleado.Departamento.DepartamentoID}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Empleado registrado correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static Result Delete(int EmpleadoID)
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.RgutierrezEstructuraContext context = new AccesoDatos.RgutierrezEstructuraContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {EmpleadoID}");
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Empleado eliminado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }


    }
}
