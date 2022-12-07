using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMvc.Models
{
    public class Actors_Movies
    {
        //RelationShip :
        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

    }
}
