using CinemaMvc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static CinemaMvc.Data.EnumClasses;

namespace CinemaMvc.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "The Full Name must be in 3 to 60 Characters")]
        public string Name { get; set; }
        public string Discription { get; set; }

        [Required(ErrorMessage = "Price is Required !")]
        public decimal Price { get; set; }

        [Display(Name = "Image Movie")]
        [Required(ErrorMessage = "ImageURL is Required !")]
        public string ImageURL { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string MovieCategory { get; set; }
       
        //RelationShip :
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public virtual Producer Producer  { get; set; }

        public virtual IEnumerable<Actors_Movies> Actors_Movies { get; set; }

    }
}
