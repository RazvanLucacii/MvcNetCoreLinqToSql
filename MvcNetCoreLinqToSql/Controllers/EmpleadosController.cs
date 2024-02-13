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
    }
}
