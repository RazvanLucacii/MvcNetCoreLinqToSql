using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSql.Models;
using MvcNetCoreLinqToSql.Repositories;

namespace MvcNetCoreLinqToSql.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repoEnf;

        public EnfermosController()
        {
            this.repoEnf = new RepositoryEnfermos();
        }
        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repoEnf.GetEnfermos();
            return View(enfermos);
        }

        public IActionResult Details(string inscripcion)
        {
            Enfermo enfermo = this.repoEnf.FindEnfermo(inscripcion);
            return View(enfermo);
        }

        public async Task<IActionResult> Delete(string inscripcion)
        {
            await this.repoEnf.DeleteAsync(inscripcion);
            return RedirectToAction("Index");
        }
    }
}
