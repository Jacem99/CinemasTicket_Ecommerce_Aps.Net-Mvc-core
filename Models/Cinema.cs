using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMvc.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
  
        public string CinemaLogo { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "The Full Name must be in 3 to 60 Characters")]
        public string Name { get; set; }

        public string Discription { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}
