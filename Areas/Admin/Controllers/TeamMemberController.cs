using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.IO;
using WebFrontToBack.Areas.Admin.ViewModels;
using WebFrontToBack.DAL;
using WebFrontToBack.Migrations;
using WebFrontToBack.Models;
using WebFrontToBack.Utilities.Extensions;

namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamMemberController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Models.TeamMember> teamMembers = await _context.TeamMembers.ToListAsync();
            return View(teamMembers);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamMemberVM team)
        {
            if (!ModelState.IsValid)
            {
				return View();
			}
            if (!team.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{team.Photo.FileName}must be image type");
                return View();
            }
            if (!team.Photo.CheckFileSize(500))
            {
                ModelState.AddModelError("Photo", $"{team.Photo.FileName} - file must be size less than 200kb ");
                return View();

			}
            string root =Path.Combine(_webHostEnvironment.WebRootPath,"assets","img" );

            string fileName = await team.Photo.SaveAsync(root);
            TeamMember teamMember = new TeamMember()
            {
                FulName = team.FulName,
                Path=fileName,
                Profection=team.Profection,
            };
            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }






        //     public IActionResult Edit(int id)
        //     {
        //TeamMember? teamMember = _context.TeamMembers.Find(id);
        //CreateTeamMemberVM createTeamMemberVM = new CreateTeamMemberVM()
        //         {
        //	TeamMembers = teamMember

        //         };
        //         return View(createTeamMemberVM);

        //     }

        public IActionResult Edit(int id)
        {

			TeamMember? teamMember = _context.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }
        //[HttpPost]
        //public IActionResult Edit(TeamMember team)
        //{
        //    TeamMember? member = _context.TeamMembers.Find(team.Id);
        //    if (member == null)
        //    {
        //        return NotFound();
        //    }
        //    member.FulName = team.FulName;
        //    member.Profection = team.Profection;
        //    member.Path = team.Path;
        //    _context.TeamMembers.Update(member);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}











        public async Task<IActionResult> Delete(int id)
		{
			TeamMember teamMember = await _context.TeamMembers.FindAsync(id);
			if (teamMember == null) return NotFound();
			string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", teamMember.Path);
			if (System.IO.File.Exists(imagePath))
			{
				System.IO.File.Delete(imagePath);
			}

			_context.TeamMembers.Remove(teamMember);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}



	}
}
