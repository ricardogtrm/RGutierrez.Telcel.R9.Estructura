using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Departamentos { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (AccesoDatos.RgutierrezEstructuraContext context = new AccesoDatos.RgutierrezEstructuraContext())
                {
                    var query = context.Departamentos.FromSqlRaw("DepartamentoGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            Departamento departamento = new Departamento();
                            departamento.DepartamentoID = item.DepartamentoId;
                            departamento.Descripcion = item.Descripcion;

                            result.Objects.Add(departamento);
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
