using LeaveApplication.Migrations;
using LeaveApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveApplication.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LeaveController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var leaves = await dbContext.LeaveRequests.ToListAsync();
            return View(leaves);
        }

        public IActionResult Create()
        {
            List<SelectListItem> leaveTypes = new()
            {
                new SelectListItem { Value = "Sick Leave", Text = "Sick Leave" },
                new SelectListItem { Value = "Casual Leave", Text = "Casual Leave" },
                new SelectListItem { Value = "Annual Leave", Text = "Annual Leave" }
            };

            ViewBag.LeaveType = leaveTypes;
            return View();
        }

        // POST: Leave/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequest leave)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(leave);
                await dbContext.SaveChangesAsync();
                TempData["insert_success"] = "Leave applied successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(leave);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var leave = await dbContext.LeaveRequests.FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null) return NotFound();

            return View(leave);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var leave = await dbContext.LeaveRequests.FindAsync(id);
            if (leave == null) return NotFound();

            List<SelectListItem> leaveTypes = new()
            {
                new SelectListItem { Value = "Sick Leave", Text = "Sick Leave" },
                new SelectListItem { Value = "Casual Leave", Text = "Casual Leave" },
                new SelectListItem { Value = "Annual Leave", Text = "Annual Leave" }
            };

            ViewBag.LeaveType = leaveTypes;
            return View(leave);
        }

        // POST: Leave/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveRequest leave)
        {
            if (id != leave.Id) return NotFound();

            if (ModelState.IsValid)
            {
                dbContext.Update(leave);
                await dbContext.SaveChangesAsync();
                TempData["update_success"] = "Leave updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(leave);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var leave = await dbContext.LeaveRequests.FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null) return NotFound();

            return View(leave);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leave = await dbContext.LeaveRequests.FindAsync(id);
            if (leave != null)
            {
                dbContext.LeaveRequests.Remove(leave);
                await dbContext.SaveChangesAsync();
            }

            TempData["delete_success"] = "Leave deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
