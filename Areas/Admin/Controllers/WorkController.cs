using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WebFrontToBack.Areas.Admin.ViewModels;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using WebFrontToBack.Utilities.Constants;
using WebFrontToBack.Utilities.Extensions;

namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkController : Controller
    {
        private readonly AppDbContext _context;
        private List<WorkCategory> _workcategories;
        private readonly IWebHostEnvironment _enviroment;
        private string _errorMessages;
        public WorkController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _enviroment = environment;
            _workcategories = _context.WorkCategories.ToList();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Works
                .Where(w => !w.IsDeleted)
                .Include(w => w.WorkCategories)
                .Include(w => w.WorkImages)
                .ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            CreateWorkVM createWorkVM = new CreateWorkVM()
            {
                WorkCategories = _workcategories
            };
            return View(createWorkVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkVM createWorkVM)
        {
            createWorkVM.WorkCategories = _workcategories;
            if (!ModelState.IsValid)
            {
                return View(createWorkVM);
            }
            if (!CheckPhoto(createWorkVM.Photos)) ModelState.AddModelError("Photos", _errorMessages);
            string rootPath = Path.Combine(_enviroment.WebRootPath, "assets", "img");
            List<WorkImage> workImages = await CreateFileAndGetServiceImages(createWorkVM.Photos, rootPath);

            Work work = new Work()
            {
                Name = createWorkVM.Name,
                WorkCategoryId = createWorkVM.WorkCategoryId,
                Description = createWorkVM.Description,
                WorkImages = workImages
            };
            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            return View(createWorkVM);
        }

        private async Task< List<WorkImage>> CreateFileAndGetServiceImages(List<IFormFile> files , string rootPath)
        {
            List<WorkImage> workImages = new List<WorkImage>();
            foreach (IFormFile photo in files)
            {
                string filename = await photo.SaveAsync(rootPath);
                WorkImage Image = new WorkImage() { Path = filename };
                if (!workImages.Any(i=>i.IsActive))
                {
                    Image.IsActive = true;
                }
                workImages.Add(Image);
            }
            return workImages;
        }
        private bool CheckPhoto(List<IFormFile> workImages)
        {
            foreach (IFormFile file in workImages)
            {
                if (!file.CheckContentType("image/"))
                {
                    _errorMessages = $"{file.FileName}- {Messages.FileTypeMustBeText}";
                    return false;
                }
                if (!file.CheckFileSize(600))
                {
                    _errorMessages = $"{file.FileName} - {Messages.FileSizeMustBe200KB}";
                }

            }
            return true;
        }

        //public async Task<IActionResult> Delete(int Id)
        //{
        //    Work work = _context.Works.Find(Id);
        //    if (work == null) return NotFound();
        //    string imagePath = Path.Combine(_enviroment.WebRootPath, "assets", "img", work);
        //    if (System.IO.File.Exists(imagePath))
        //    {
        //        System.IO.File.Delete(imagePath);
        //    }
        //    _context.Works.Remove(work);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}
