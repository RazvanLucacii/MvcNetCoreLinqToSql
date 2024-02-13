using MvcNetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcNetCoreLinqToSql.Repositories
{
    public class RepositoryEmpleados
    {
        private DataTable tablaEmpleados;

        public RepositoryEmpleados()
        {
            string sql = "select * from EMP";
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            SqlDataAdapter adEmp = new SqlDataAdapter(sql, connectionString);
            this.tablaEmpleados = new DataTable();
            adEmp.Fill(tablaEmpleados);
        }

        public ResumenEmpleados GetEmpleadosOficio(string oficio)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio
                           select datos;
            consulta = consulta.OrderBy( empleadoRow => empleadoRow.Field<int>("SALARIO"));
            int personas = consulta.Count();
            int maximo = consulta.Max(z => z.Field<int>("SALARIO"));
            double media = consulta.Average(x => x.Field<int>("SALARIO"));
            List<Empleado> empleados = new List<Empleado>();
            foreach (var row in consulta)
            {
                Empleado empleado = new Empleado
                {
                    IdEmpleado = row.Field<int>("EMP_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Oficio = row.Field<string>("OFICIO"),
                    Salario = row.Field<int>("SALARIO"),
                    IdDepartamento = row.Field<int>("DEPT_NO")
                };
                empleados.Add(empleado);
            }
            ResumenEmpleados resumen = new ResumenEmpleados
            {
                Personas = personas,
                MaxSalario = maximo,
                MediaSalarial = media,
                Empleados = empleados
            };
            return resumen;
        }

        public ResumenEmpleados GetEmpleadosDepartamento(int departamento)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<int>("DEPT_NO") == departamento
                           select datos;
            consulta = consulta.OrderBy(empleadoRow => empleadoRow.Field<int>("SALARIO"));
            int personas = consulta.Count();
            int maximo = consulta.Max(z => z.Field<int>("SALARIO"));
            double media = consulta.Average(x => x.Field<int>("SALARIO"));
            List<Empleado> empleados = new List<Empleado>();
            foreach (var row in consulta)
            {
                Empleado empleado = new Empleado
                {
                    IdEmpleado = row.Field<int>("EMP_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Oficio = row.Field<string>("OFICIO"),
                    Salario = row.Field<int>("SALARIO"),
                    IdDepartamento = row.Field<int>("DEPT_NO")
                };
                empleados.Add(empleado);
            }
            ResumenEmpleados resumen = new ResumenEmpleados
            {
                Personas = personas,
                MaxSalario = maximo,
                MediaSalarial = media,
                Empleados = empleados
            };
            return resumen;
        }

        public List<string> GetOficios()
        {
            var consulta = (from datos in this.tablaEmpleados.AsEnumerable()
                           select datos.Field<string>("OFICIO")).Distinct();
            List<string> oficios = new List<string>();
            foreach (string ofi in consulta)
            {
                oficios.Add(ofi);
            }
            return oficios;
        }

        public List<int> GetDepartamentos()
        {
            var consulta = (from datos in this.tablaEmpleados.AsEnumerable()
                            select datos.Field<int>("DEPT_NO")).Distinct();
            List<int> departamento = new List<int>();
            foreach (int dept in consulta)
            {
                departamento.Add(dept);
            }
            return departamento;
        }

        public List<Empleado> GetEmpleados()
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable() 
                           select datos;
            List<Empleado> empleados = new List<Empleado>();
            foreach (var row in consulta)
            {
                Empleado emp = new Empleado();
                emp.IdEmpleado = row.Field<int>("EMP_NO");
                emp.Apellido = row.Field<string>("APELLIDO");
                emp.Oficio = row.Field<string>("OFICIO");
                emp.Salario = row.Field<int>("SALARIO");
                emp.IdDepartamento = row.Field<int>("DEPT_NO");
                empleados.Add(emp);
            }
            return empleados;
        }

        public Empleado FindEmpleado(int idEmpleado)
        {
            //alias datos representa cada linea/objeto dentro del conjunto
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<int>("EMP_NO") == idEmpleado
                           select datos;
            var row = consulta.First();
            Empleado empleado = new Empleado();
            empleado.IdEmpleado = row.Field<int>("EMP_NO");
            empleado.Apellido = row.Field<string>("APELLIDO");
            empleado.Oficio = row.Field<string>("OFICIO");
            empleado.Salario = row.Field<int>("SALARIO");
            empleado.IdDepartamento = row.Field<int>("DEPT_NO");
            return empleado;

        }

        public List<Empleado> GetEmpleadosOficioSalario(string oficio, int salario)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio
                           && datos.Field<int>("SALARIO") >= salario
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Empleado> empleados = new List<Empleado>();
                foreach(var row in consulta)
                {
                    Empleado empleado = new Empleado
                    {
                        IdEmpleado = row.Field<int>("EMP_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Oficio = row.Field<string>("OFICIO"),
                        Salario = row.Field<int>("SALARIO"),
                        IdDepartamento = row.Field<int>("DEPT_NO")
                    };
                    empleados.Add(empleado);
                }
                return empleados;
            }
        }
    }
}
