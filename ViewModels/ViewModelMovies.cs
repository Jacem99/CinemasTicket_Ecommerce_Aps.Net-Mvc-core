
using CinemaMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaMvc.ViewModels
{
    public class ViewModelMovies 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        [MaxLength(60 , ErrorMessage = "Full Name deosn't must more than 60 character") ,MinLength(3 ,ErrorMessage = "Full Name deosn't must less than 3 character")]
        public string Name { get; set; }
        public string Discription { get; set; }

        [Required(ErrorMessage = "Price is Required !")]
        public decimal Price { get; set; }

        [Display(Name = "Image Movie")]
        public string ImageURL { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string MovieCategory { get; set; }


        [Display(Name= "Actors")]
        public int ActorId { get; set; }
        public IEnumerable<Actor> actors { get; set; }


        [Display(Name = "Prodsucer")]
        public int ProducerId { get; set; }
        public IEnumerable<Producer> producers { get; set; }


        [Display(Name = "Cinemas")]
        public int CinemaId { get; set; }
        public IEnumerable<Cinema> cinemas { get; set; }
    }
}
