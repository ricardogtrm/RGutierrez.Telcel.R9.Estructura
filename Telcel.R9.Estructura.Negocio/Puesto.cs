using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Puesto
    {
        public int PuestoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Puestos { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.RgutierrezEstructuraContext context = new AccesoDatos.RgutierrezEstructuraContext())
                {
                    var query = context.Puestos.FromSqlRaw("PuestoGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            Puesto puesto = new Puesto();
                            puesto.PuestoID = item.PuestoId;
                            puesto.Descripcion = item.Descripcion;

                            result.Objects.Add(puesto);
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
    }
}
