using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        [Route("Categoria/Categoria")]
        public ActionResult CategoriaAdministrar()
        {
            return View();
        }

        [Route("Categoria/Registrar")]
        public ActionResult CategoriaRegistrar(string tipo)
        {

            return View();
        }
    }
}
