using CinemaMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMvc.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly ApplicationDbContext _context;

        public ActorsService(ApplicationDbContext appDbContext)
        {
            _context = appDbContext;
        }

       /* public void Add(Actor actor)
        {
            var addActor = _context.Actors.Add(actor);
             _context.SaveChanges();
        }*/

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var result =await _context.Actors.ToListAsync();
            return (result);
        }

        public async Task< Actor> GetById(int id)
        {
            var actor =await _context.Actors.FindAsync(id);
            return actor;
                }

        public Actor Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
