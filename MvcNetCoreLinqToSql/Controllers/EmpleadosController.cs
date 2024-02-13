using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSql.Models;
using MvcNetCoreLinqToSql.Repositories;

namespace MvcNetCoreLinqToSql.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repoEmp;

        public EmpleadosController()
        {
            this.repoEmp = new RepositoryEmpleados();
        }

        public IActionResult DatosEmpleados()
        {
            ViewData["OFICIOS"] = this.repoEmp.GetOficios();
            ViewData["DEPARTAMENTOS"] = this.repoEmp.GetDepartamentos();
            return View();
        }

        [HttpPost]
        public IActionResult DatosEmpleados(string? oficio, int departamento)
        {
            ViewData["OFICIOS"] = this.repoEmp.GetOficios();
            ViewData["DEPARTAMENTOS"] = this.repoEmp.GetDepartamentos();
            ResumenEmpleados model = null;

            if (oficio != null)
            {
                model = this.repoEmp.GetEmpleadosOficio(oficio);
            }
            if (departamento != 0)
            {
                model = this.repoEmp.GetEmpleadosDepartamento(departamento);
            }

            return View(model);
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repoEmp.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Detalles(int id)
        {
            Empleado empleado = this.repoEmp.FindEmpleado(id);
            return View(empleado);
        }

        public IActionResult BuscarEmpleados()
        {
            List<Empleado> empleados = this.repoEmp.GetEmpleados();
            return View(empleados);
        }

        [HttpPost]
        public IActionResult BuscarEmpleados(string oficio, int salario)
        {
            List<Empleado> empleados = this.repoEmp.GetEmpleadosOficioSalario(oficio, salario);
            if (empleados == null)
            {
                ViewData["MENSAJE"] = "No existen registros con oficio " + oficio + " ni salario mayor a " + salario;
                return View();
            }
            else
            {
                return View(empleados);
            }
        }
    }
}
