
using CinemaMvc.Data;
using CinemaMvc.Data.Services;
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
    public class ActorsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IActorsService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly new List<string> _createExtention = new List<string> { ".jpg", ".png", ".jpeg" };
        public ActorsController(IActorsService actorsService, ApplicationDbContext appDbContext , IWebHostEnvironment webHostEnvironment
            )
        {
            _service = actorsService;
            _webHostEnvironment = webHostEnvironment;
            _context = appDbContext;
        }

        public async Task< Actor> Find(int? Id)
        {
           return await _context.Actors.FindAsync(Id);
        }
        public async Task<IActionResult> Index()
        {
            return View (await _service.GetAll());
        }
        public IActionResult Create()
        {

            return View(new Actor()) ;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Actor actor)
        {
            if (actor == null)
                return BadRequest();

            if (actor.FullName == null)
            {
                ModelState.AddModelError("FullName", "Full Name is Required !"); 
                return View(actor);
            }

            IFormFileCollection files = Request.Form.Files;
               
            if (!files.Any())
            {
                ModelState.AddModelError("ProfilePictureURL", "Profile Picture is Required !");
                return View(actor);
            }
            actor.ProfilePictureURL = files[0].FileName;

            var checkPhoto = await _context.Actors.SingleOrDefaultAsync(a => a.ProfilePictureURL == actor.ProfilePictureURL);
            if (checkPhoto != null)
            {
                ModelState.AddModelError("ProfilePictureURL", "File is already existed !");
                return View(actor);
            }

            var fileExtentions = Path.GetExtension(files[0].FileName);
            

            if (!_createExtention.Contains(fileExtentions.ToLower()))
            {
                ModelState.AddModelError("ProfilePictureURL", "The Extension of Picture must be jpg/png/jpeg");
                return View(actor);
            }

            string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images","actors", $"{files[0].FileName}");

            var file_Stream = new FileStream(pathCombine, FileMode.Create);
              await files[0].CopyToAsync(file_Stream);
              file_Stream.Close();
    
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if(Id == null)
                return BadRequest();

            var actor = await Find(Id);

            if (actor == null)
                return NotFound();

            return View("Create",actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Actor actor)
        {

            if (actor.Id < 1)
                return BadRequest();

            if (actor == null)
                return BadRequest();

            var infoActor = await Find(actor.Id);
            if (infoActor == null)
                return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                actor.ProfilePictureURL = files[0].FileName;

                var checkPhoto = await _context.Actors.SingleOrDefaultAsync(a => a.ProfilePictureURL == actor.ProfilePictureURL);
                if (checkPhoto != null)
                {
                    ModelState.AddModelError("ProfilePictureURL", "File is already existed !");
                    return View("Create", actor);
                }
                var fileExtentions = Path.GetExtension(files[0].FileName);
                if (!_createExtention.Contains(fileExtentions.ToLower()))
                {
                    ModelState.AddModelError("ProfilePictureURL", "The Extension of Picture must be jpg/png/jpeg");
                    return View("Create",actor);
                }

                string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "actors", $"{files[0].FileName}");

                var file_Stream = new FileStream(pathCombine, FileMode.Create);
                await files[0].CopyToAsync(file_Stream);
                file_Stream.Close();
                infoActor.ProfilePictureURL = actor.ProfilePictureURL;
            }

            if (actor.FullName == null)
            {
                ModelState.AddModelError("FullName", "The Full Name is Required");
                return View("Create", actor);
            }

            infoActor.FullName = actor.FullName;
            infoActor.Bio = actor.Bio;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            var actor = await Find(Id);
            if(actor == null)
            {
                return NotFound("Not Found !");
            }

            string localPath = _webHostEnvironment.WebRootPath ;
           
            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "actors", $"{actor.ProfilePictureURL}");
            FileInfo  fi = new FileInfo(pathCombine);
            if (fi != null)
            {
            System.IO.File.Delete(pathCombine);
                fi.Delete();
            }
            _context.Actors.Remove(actor);
           await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
