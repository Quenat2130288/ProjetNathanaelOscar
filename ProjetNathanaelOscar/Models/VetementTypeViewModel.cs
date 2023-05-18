using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProjetNathanaelOscar.Models
{
    public class VetementTypeViewModel
    {
        public List<Vetement>? Vetements { get; set; }
        public SelectList? TypeVetement { get; set; }
        public string? VetementType { get; set; }
        public string? SearchString { get; set; }
    }
}
