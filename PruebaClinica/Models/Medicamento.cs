using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaClinica.Models
{
    public class Medicamento
    {
        [Display(Name = "Codigo de Medicamento")]
        public int Id { get; set; }

        [Display(Name = "Nombre de Medicamento")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string NombreMedicamento { get; set; }

        [Display(Name = "Fecha de recibido")]
        [Required(ErrorMessage = "Debe ingresar la fecha de recibido")]
        public DateTime FechaRecibido { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor del medicamento")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad")]
        public int Cantidad { get; set; }
    }

}
