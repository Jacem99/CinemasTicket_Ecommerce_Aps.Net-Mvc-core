
using CinemaMvc.Data;
using CinemaMvc.Models;
using CinemaMvc.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cinemas.Controllers
{
    public class MoviesController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly new List<string> _createExtention = new List<string> { ".jpg", ".png", ".jpeg" };
        public MoviesController(ApplicationDbContext appDbContext , IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = appDbContext;
        }
        public async Task<IActionResult> Index(string searching)
        {
            if(String.IsNullOrEmpty(searching))
            {
                var movie = await _context.Movies
              .Include(c => c.Cinema).OrderBy(s => s.Name)
                .ToListAsync();
                return View(movie);
            }
          
            var modify_search = searching.ToLower().Trim();
            var searchigMovie = await _context.Movies
                .Where(m => m.Name.Contains(modify_search) || m.MovieCategory.Contains(modify_search) || m.Cinema.Name.Contains(modify_search) )
                .Include(c=>c.Cinema).OrderBy(s => s.Name)
                .ToListAsync();
            return View(searchigMovie);
        }
        private async Task< ViewModelMovies> getViewModelMovies()
        {
            ViewModelMovies viewMovie = new ViewModelMovies()
            {
                actors = await _context.Actors.ToListAsync(),
                producers = await _context.producers.ToListAsync(),
                cinemas = await _context.Cinemas.ToListAsync()
            };
            return viewMovie;
        }
        public async Task< IActionResult> Create()
        {  
            return View(await getViewModelMovies());
        }
        public async Task< IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var viewModelMov = await getViewModelMovies();
            var movie =  await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            ViewModelEditMovie viewModelEditMovie = new ViewModelEditMovie()
            {
                movie = movie,
                actors = viewModelMov.actors,
                cinemas = viewModelMov.cinemas,
                producers = viewModelMov.producers,
            };
            return View(viewModelEditMovie);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(ViewModelMovies viewModelMovies)
        {
            if (viewModelMovies == null)
                return BadRequest();

            if (viewModelMovies.Name == null)
            {
                ModelState.AddModelError("Name", " Name is Required !");
                return View(await getViewModelMovies());
            }

            IFormFileCollection files = Request.Form.Files;

            if (!files.Any())
            {
                ModelState.AddModelError("ImageURL", "Picture is Required !");
                return View(await getViewModelMovies());
            }
            viewModelMovies.ImageURL = files[0].FileName;

            var checkPhoto = await _context.Movies.SingleOrDefaultAsync(a => a.ImageURL == viewModelMovies.ImageURL);
            if (checkPhoto != null)
            {
                ModelState.AddModelError("ImageURL", "File is already existed !");
                return View(await getViewModelMovies());
            }

            var fileExtentions = Path.GetExtension(files[0].FileName);


            if (!_createExtention.Contains(fileExtentions.ToLower()))
            {
                ModelState.AddModelError("ImageURL", "The Extension of Picture must be jpg/png/jpeg");
                return View(await getViewModelMovies());
            }

            string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "films", $"{files[0].FileName}");

            var file_Stream = new FileStream(pathCombine, FileMode.Create);
            await files[0].CopyToAsync(file_Stream);
            file_Stream.Close();

            Movie movie= new Movie()
            {
                Name = viewModelMovies.Name,
                Discription = viewModelMovies.Discription,
                Price = viewModelMovies.Price,
                StartDate = viewModelMovies.StartDate,
                EndDate = viewModelMovies.EndDate,
                CinemaId = viewModelMovies.CinemaId,
                ProducerId = viewModelMovies.ProducerId,
                ImageURL = viewModelMovies.ImageURL,
                MovieCategory = ((EnumClasses.MovieCategory)Int16.Parse(viewModelMovies.MovieCategory)).ToString(),
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ViewModelMovies viewModelMovies)
        {

            if (viewModelMovies.Id < 1)
                return BadRequest();

            if (viewModelMovies == null)
                return BadRequest();

            var infoMovie = await _context.Movies.FindAsync(viewModelMovies.Id);

            if (infoMovie == null)
                return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                viewModelMovies.ImageURL = files[0].FileName;

                var checkPhoto = await _context.Cinemas.AnyAsync(a => a.CinemaLogo == viewModelMovies.ImageURL);
                if (checkPhoto)
                {
                    ModelState.AddModelError("ImageURL", "File is already existed !");
                    return View("Create", viewModelMovies);
                }
                var fileExtentions = Path.GetExtension(files[0].FileName);
                if (!_createExtention.Contains(fileExtentions.ToLower()))
                {
                    ModelState.AddModelError("ImageURL", "The Extension of Picture must be jpg/png/jpeg");
                    return View("Create", viewModelMovies);
                }

                string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "films", $"{files[0].FileName}");

                var file_Stream = new FileStream(pathCombine, FileMode.Create);
                await files[0].CopyToAsync(file_Stream);
                file_Stream.Close();
                infoMovie.ImageURL = viewModelMovies.ImageURL;
            }

            if (viewModelMovies.Name == null)
            {
                ModelState.AddModelError("Name", "The Name is Required");
                return View("Create", viewModelMovies);
            }
            if (viewModelMovies.Price <0)
            {
                ModelState.AddModelError("Price", "The Price is Required");
                return View("Create", viewModelMovies);
            }

            infoMovie.Name = viewModelMovies.Name;
            infoMovie.Price = viewModelMovies.Price;
            infoMovie.Discription = viewModelMovies.Discription;
            infoMovie.StartDate = viewModelMovies.StartDate;
            infoMovie.EndDate = viewModelMovies.EndDate;
            infoMovie.StartDate = viewModelMovies.StartDate;
            infoMovie.ProducerId = viewModelMovies.ProducerId;
            infoMovie.CinemaId = viewModelMovies.CinemaId;
            infoMovie.MovieCategory = ((EnumClasses.MovieCategory)Int16.Parse(viewModelMovies.MovieCategory)).ToString();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id ==null)
                return BadRequest();

            var movie = await _context.Movies
                .Include(c => c.Cinema).Include(c => c.Producer).SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound("Not Found Item !");

            return View(movie);
        }
    }
}
