using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMvc.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture ")]
      
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name ")]
        [Required(ErrorMessage = "Full Name is Required !")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "The Full Name must be in 3 to 60 Characters")]
        public string FullName { get; set; }
        public string Bio { get; set; }

        //RelationShip :
        public virtual IEnumerable<Movie> Movies { get; set; }
    }
}
