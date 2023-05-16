using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        public TeamMemberController(AppDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create(TeamMember team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _context.TeamMembers.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            TeamMember? teamMember = _context.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }
        [HttpPost]
        public IActionResult Edit(TeamMember team)
        {
            TeamMember? member = _context.TeamMembers.Find(team.Id);
            if (member == null)
            {
                return NotFound();
            }
            member.FulName = team.FulName;
            member.Profection = team.Profection;
            member.Path = team.Path;
            _context.TeamMembers.Update(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            TeamMember member = _context.TeamMembers.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            _context.TeamMembers.Remove(member);
            _context.SaveChanges(); 
            return RedirectToAction("Index");
        }

      

    }
}
