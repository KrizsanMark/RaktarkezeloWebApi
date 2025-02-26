using System.ComponentModel.DataAnnotations;

namespace RaktarkezeliWebApi.Models
{
    public class Beszallitok
    {
        public int Id { get; set; }

        [Required]
        
        public string Name { get; set; } 

        public ICollection<Termekek>? Termekeks { get; set; }
    }
}
