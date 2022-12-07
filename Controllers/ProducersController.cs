
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
    public class ProducersController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly new List<string> _createExtention = new List<string> { ".jpg", ".png", ".jpeg" };

        public ProducersController(ApplicationDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Producer> Find(int? Id)
        {
            return await _context.producers.FindAsync(Id);
        }
        public async Task< IActionResult> Index()
        {
            return View(await _context.producers.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Producer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (producer.FullName == null)
            {
                ModelState.AddModelError("FullName", "FullName is Required !");
                return View(producer);
            }

            IFormFileCollection files = Request.Form.Files;

            if (!files.Any())
            {
                ModelState.AddModelError("ProfilePictureURL", "ProfilePictureURL is Required !");
                return View(producer);
            }
            producer.ProfilePictureURL = files[0].FileName;

            var checkPhoto = await _context.producers.AnyAsync(a => a.ProfilePictureURL == producer.ProfilePictureURL);
            if (checkPhoto)
            {
                ModelState.AddModelError("ProfilePictureURL", "File is already existed !");
                return View(producer);
            }

            var fileExtentions = Path.GetExtension(files[0].FileName);

            if (!_createExtention.Contains(fileExtentions.ToLower()))
            {
                ModelState.AddModelError("ProfilePictureURL", "The Extension of Picture must be jpg/png/jpeg");
                return View(producer);
            }

            string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "producers", $"{files[0].FileName}");

            var file_Stream = new FileStream(pathCombine, FileMode.Create);
            await files[0].CopyToAsync(file_Stream);
            file_Stream.Close();

            await _context.producers.AddAsync(producer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var producer = await Find(Id);

            if (producer == null)
                return NotFound();

            return View("Create",producer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Producer producer)
        {

            if (producer.Id < 1)
                return BadRequest();

            if (producer == null)
                return BadRequest();

            var infoProducer = await Find(producer.Id);
            if (infoProducer == null)
                return NotFound();

            
            var files = Request.Form.Files;
            if (files.Any())
            {
                     producer.ProfilePictureURL = files[0].FileName;

                var checkPhoto = await _context.producers.AnyAsync(a => a.ProfilePictureURL == producer.ProfilePictureURL);
                if (checkPhoto)
                {
                    ModelState.AddModelError("ProfilePictureURL", "File is already existed !");
                    return View("Create", producer);
                }
                var fileExtentions = Path.GetExtension(files[0].FileName);
                if (!_createExtention.Contains(fileExtentions.ToLower()))
                {
                    ModelState.AddModelError("ProfilePictureURL", "The Extension of Picture must be jpg/png/jpeg");
                    return View("Create", producer);
                }

                string localPath = _webHostEnvironment.WebRootPath + "\\images\\";

                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "producers", $"{files[0].FileName}");

                var file_Stream = new FileStream(pathCombine, FileMode.Create);
                await files[0].CopyToAsync(file_Stream);
                file_Stream.Close();
                infoProducer.ProfilePictureURL = producer.ProfilePictureURL;
            }

            if (producer.FullName == null)
            {
                ModelState.AddModelError("FullName", "The Full Name is Required");
                return View("Create", producer);
            }

            infoProducer.FullName = producer.FullName;
            infoProducer.Bio = producer.Bio;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            var producer = await Find(Id);
            if (producer == null)
            {
                return NotFound("Not Found !");
            }

            string localPath = _webHostEnvironment.WebRootPath;

            var pathCombine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "producers", $"{producer.ProfilePictureURL}");
            FileInfo fi = new FileInfo(pathCombine);
            if (fi != null)
            {
                System.IO.File.Delete(pathCombine);
                fi.Delete();
            }
            _context.producers.Remove(producer);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
