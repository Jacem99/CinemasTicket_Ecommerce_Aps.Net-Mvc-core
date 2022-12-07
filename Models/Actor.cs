using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMvc.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
       
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }

        [Required(ErrorMessage = "Full Name is Required !")]
        [Display(Name = "Full Name")]
        [StringLength(120,MinimumLength =6, ErrorMessage ="The Full Name must be in 6 to 120 Characters")]
        public string FullName { get; set; }
        public string Bio { get; set; }

        //RealationShip
        public IEnumerable<Actors_Movies> Actors_Movies { get; set; }

    }
}
