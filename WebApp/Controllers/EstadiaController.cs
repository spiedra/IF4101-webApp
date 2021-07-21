using WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using WebApp.Utility;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EstadiaController : Controller
    {
        [Route("Estadia/Registrar")]
        public ActionResult EstadiaRegistrar(string message)
        {
            ConnectionDb connectionDb = new();
            ViewBag.message = message;
            ViewBag.CategoryTypes = Utils.ExcGetList(connectionDb);
            return View();
        }

        [Route("Estadia/Registrar_")]
        public ActionResult EstadiaRegistrar(EstadiaViewModel estadiaViewModel)
        {
            ConnectionDb connectionDb = new();
            Utils.ExcRegisterEstadia(connectionDb, estadiaViewModel);
            return RedirectToAction(nameof(EstadiaRegistrar), new { message = "Registrada correctamente" });
        }

        [Route("Estadia/Administrar")]
        public ActionResult EstadiaAdministrar()
        {
            return View();
        }

        [Route("Estadia/Administrar_")]
        public JsonResult EstadiaAdministrar(string nombreEstancia)
        {
            if (!string.IsNullOrEmpty(nombreEstancia))
            {
                ConnectionDb connectionDb = new();
                return Json(Utils.ExcGetEstanciaByNombre(connectionDb, nombreEstancia));
            }
            return Json(false);
        }

        [Route("Estadia/Eliminar")]
        public JsonResult EstadiaEliminar(int estanciaId)
        {
            ConnectionDb connectionDb = new();
            Utils.ExcDeleteEstancia(connectionDb, estanciaId);
            return Json("Eliminado correctamente");
        }

        [Route("Estadia/Categorias")]
        public JsonResult GetCategorias()
        {
            ConnectionDb connectionDb = new();
            return Json(Utils.ExcGetList(connectionDb));
        }

        [Route("Estadia/Actualizar")]
        public JsonResult EstanciaActualizar(EstadiaViewModel estadiaViewModel)
        {
            ConnectionDb connectionDb = new();
            Utils.ExcActualizarEstadia(connectionDb, estadiaViewModel);
            return Json("Actualizado con exito");
        }
    }
}
