using System.ComponentModel.DataAnnotations;

namespace RaktarkezeliWebApi.Models
{
    public class Termekek
    {
        public int Id { get; set; }

        [Required]
       
        public string Name { get; set; } 

        [Required]
        public decimal Ar { get; set; }

        public int BeszallitokId { get; set; } // Külső kulcs
        public Beszallitok? Beszallitoks { get; set; } // Navigációs property
    }
}
