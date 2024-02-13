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
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            string sql = "select * from EMP";
            SqlDataAdapter adEmp = new SqlDataAdapter(connectionString, sql);
            this.tablaEmpleados = new DataTable();
            adEmp.Fill(tablaEmpleados);
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
    }
}
