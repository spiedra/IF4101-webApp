using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        [Route("Categoria/Categoria")]
        public ActionResult CategoriaAdministrar(string message)
        {
           ConnectionDb connectionDb = new();
           ViewBag.message = message;
           ViewData["categorias"] = Utils.ExcGetListCategorias(connectionDb);
            return View();
        }

        [Route("Categoria/Registrar")]
        public ActionResult CategoriaRegistrar(CategoriaViewModel categoriaViewModel)
        {
            ConnectionDb connectionDb = new();
            if (Utils.ExcRegisterCategoria(connectionDb, categoriaViewModel))
            {
                ViewData["categorias"] = Utils.ExcGetListCategorias(connectionDb);
                return RedirectToAction(nameof(CategoriaAdministrar));
            }
            ViewBag.message = "El tipo de categoria ya ha sido registrada";
            return RedirectToAction(nameof(CategoriaAdministrar), new { message = "Tipo de categoria ya ha sido registrada" });
        }

        [Route("Categoria/Eliminar")]
        public ActionResult CategoriaEliminar(CategoriaViewModel categoriaViewModel)
        {
            ConnectionDb connectionDb = new();
            Utils.ExcDeleteCategoria(connectionDb, categoriaViewModel);
            return RedirectToAction(nameof(CategoriaAdministrar));
        }
    }
}
