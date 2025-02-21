using System.ComponentModel.DataAnnotations;

namespace EscaladaOptima.Models
{
    public class Elemento
    {
        [Key] 
        [Required] 
        public int Id { get; set; } 
        
        public required string Nombre { get; set; }
        public int Peso { get; set; }
        public int Calorias { get; set; }
    }
}