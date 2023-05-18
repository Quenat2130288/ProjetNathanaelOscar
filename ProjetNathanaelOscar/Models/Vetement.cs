using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetNathanaelOscar.Models
{
    public class Vetement
    {
        public int VetementId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string DateObtention { get; set; }
        public int Cote { get; set; }
        public string TypeVetement { get; set; }
        public string Image { get; set; }
        public string? ProprietaireId { get; set; }
    }
}
