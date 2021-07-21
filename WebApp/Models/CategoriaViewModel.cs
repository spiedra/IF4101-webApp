using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WebApp.Models
{
    public class CategoriaViewModel
    {
        [Required(ErrorMessage = "Por favor el nombre del tipo de categoría")]
        public string Tipo { get; set; }
    }
}
