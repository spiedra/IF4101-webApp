using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ReservaViewModel
    {
        public int Id { get; set; }
        public string NombreEstadia { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoContacto { get; set; }
        public string CedulaCliente { get; set; }
        public string Provincia { get; set; }
        public int CantidadPersonas { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
