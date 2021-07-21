using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class EstadiaViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre de la estancia")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre de la provincia")]
        public string? Provincia { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la dirección exacta")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el precio por noche")]
        public decimal? PrecionNoche { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la capacidad")]
        public int? Capacidad { get; set; }

        public string? RutaImagen { get; set; }

        [Required(ErrorMessage = "Por favor suba alguna imagen")]
        [DataType(DataType.Upload)]
        public IFormFile Imagen { get; set; }

        [Required(ErrorMessage = "Por favor seleccione algun tipo de categoria")]
        public string? TipoCategoria { get; set; }

        [Required(ErrorMessage = "Por favor ingrese alguna descipción")]
        public string? Descripcion { get; set; }
    }
}
