using CinemaMvc.Data;
using CinemaMvc.Models;

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
    public class CinemasController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly new List<string> _createExtention = new List<string> { ".jpg", ".png", ".jpeg" };
        public CinemasController(ApplicationDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Cinema> Find(int? Id)
        {
            return await _context.Cinemas.FindAsync(Id);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cinemas.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Cinema());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cinema cinema)
        { 
            if (cinema.Name == null)
            {
                ModelState.AddModelError("Name", "Name is Required !");
                return View(cinema);
            }

            IFormFileCollection files = Request.Form.Files;

            if (!files.Any())
            {
                ModelState.AddModelError("CinemaLogo", "CinemaLogo is Required !");
                return View(cinema);
            }
            cinema.CinemaLogo = files[0].FileName;

            var checkPhoto = await _context.Cinemas.SingleOrDefaultAsync(a => a.CinemaLogo == cinema.CinemaLogo);
            if (checkPhoto != null)
            {
                ModelState.AddModelError("CinemaLogo", "File is already existed !");
                return View(cinema);
            }

            var fileExtentions = Path.GetExtension(files[0].FileName);
          
            if (!_createExtention.Contains(fileExtentions.ToLower()))
            {
                ModelState.AddModelError("CinemaLogo", "The Extension of Picture must be jpg/png/jpeg");
                return View(cinema);
            }

            string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "cinemas", $"{files[0].FileName}");

            var file_Stream = new FileStream(pathCombine, FileMode.Create);
            await files[0].CopyToAsync(file_Stream);
            file_Stream.Close();

            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var cinema = await Find(Id);

            if (cinema == null)
                return NotFound();

            return View("Create",cinema);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cinema cinema)
        {

            if (cinema.Id < 1)
                return BadRequest();

            if (cinema == null)
                return BadRequest();

            var infoCinema = await Find(cinema.Id);
            if (infoCinema == null)
                return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                cinema.CinemaLogo = files[0].FileName;

                var checkPhoto = await _context.Cinemas.AnyAsync(a => a.CinemaLogo == cinema.CinemaLogo);
                if (checkPhoto)
                {
                    ModelState.AddModelError("CinemaLogo", "File is already existed !");
                    return View("Create", cinema);
                }
                var fileExtentions = Path.GetExtension(files[0].FileName);
                if (!_createExtention.Contains(fileExtentions.ToLower()))
                {
                    ModelState.AddModelError("CinemaLogo", "The Extension of Picture must be jpg/png/jpeg");
                    return View("Create", cinema);
                }

                string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "cinemas", $"{files[0].FileName}");

                var file_Stream = new FileStream(pathCombine, FileMode.Create);
                await files[0].CopyToAsync(file_Stream);
                file_Stream.Close();
                infoCinema.CinemaLogo = cinema.CinemaLogo;
            }

            if (cinema.Name == null)
            {
                ModelState.AddModelError("Name", "The Name is Required");
                return View("Create", cinema);
            }

            infoCinema.Name = cinema.Name;
            infoCinema.Discription = cinema.Discription;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            var cinema = await Find(Id);
            if (cinema == null)
            {
                return NotFound("Not Found !");
            }

            string localPath = _webHostEnvironment.WebRootPath;

            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "actors", $"{cinema.CinemaLogo}");
            FileInfo fi = new FileInfo(pathCombine);
            if (fi != null)
            {
                System.IO.File.Delete(pathCombine);
                fi.Delete();
            }
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
