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
    public class ReservaController : Controller
    {
        [Route("Reserva/Registrar")]
        public ActionResult ReservaRegistrar()
        {
            return View();
        }

        [Route("Reserva/Detalles")]
        public ActionResult ReservaBuscar()
        {
            return View();
        }

        [Route("Reserva/Estadias")]
        public JsonResult GetEstancias()
        {
            ConnectionDb connectionDb = new();
            return Json(Utils.ExcGetEstadias(connectionDb));
        }

        [Route("Reserva/Reservas")]
        public JsonResult GetReservas()
        {
            ConnectionDb connectionDb = new();
            return Json(Utils.ExcGetReservas(connectionDb));
        }

        [Route("Reserva/ReservasFiltrada")]
        public JsonResult GetReservas(string provincia, DateTime entrada, DateTime salida)
        {
            ConnectionDb connectionDb = new();
            if (!string.IsNullOrEmpty(provincia))
            {
                return Json(Utils.ExcGetReservas(connectionDb).Where(n =>
                n.Provincia.ToLower().Contains(provincia.ToLower()) ||
                n.FechaEntrada <= entrada &&
                n.FechaSalida >= salida
                ));
            }
            return Json(null);
        }

        [Route("Reserva/EstadiasFiltrada")]
        public JsonResult GetEstanciasFiltrada(string nombreEstadia, string provincia, int precioRango, string tipoCategoria, int cantidad)
        {

            ConnectionDb connectionDb = new();
            if (!string.IsNullOrEmpty(nombreEstadia) && !string.IsNullOrEmpty(provincia) && !string.IsNullOrEmpty(tipoCategoria))
            {
                return Json(Utils.ExcGetEstadias(connectionDb).Where(n =>
                n.Nombre.ToLower().Contains(nombreEstadia.ToLower()) ||
                n.Provincia.ToLower().Contains(provincia.ToLower()) ||
                n.PrecionNoche <= precioRango ||
                n.TipoCategoria == tipoCategoria ||
                n.Capacidad == cantidad
                ));
            }
            return Json(null);
        }

        [Route("Reserva/Registrar_")]
        public JsonResult ReservaResgistrar(int idEstancia, string cedula, string nombreCompleto, string telefono, int cantidadPersonas, string fechaEntrada, string fechaSalida)
        {
            ConnectionDb connectionDb = new();
            if (Utils.ExcRegisterReserva(connectionDb, idEstancia, cedula, nombreCompleto, telefono, cantidadPersonas, fechaEntrada, fechaSalida))
            {
                return Json("Reserva confimada correctamente");
            }
            return Json("No se ha podido completar la reserva. Revisa la cantidad de personas permitidas");
        }
    }
}
