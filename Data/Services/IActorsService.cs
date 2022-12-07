using CinemaMvc.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMvc.Data.Services
{
   public interface IActorsService
    {
        Task<IEnumerable<Actor>> GetAll();
        Task<Actor> GetById(int id);    
      //  void Add(Actor actor);
        Actor Update(int id, Actor newActor);
        void Delete(int id);
    }
}
