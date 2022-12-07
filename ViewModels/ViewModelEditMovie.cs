using CinemaMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaMvc.ViewModels
{
    public class ViewModelEditMovie
    {
     
        public Movie movie { get; set; }

        [Display(Name = "Actors")]
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
