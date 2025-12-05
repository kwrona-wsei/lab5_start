using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab5_start.Data;
using lab5_start.Models;
using lab5_start.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace lab5_start.Controllers
{
    [Authorize]
    public class GiftRequestsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        : Controller {
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);

            IQueryable<GiftRequest> query = context.GiftRequests.Include(g => g.Gift).Include(g => g.User);

            if (!User.IsInRole("Santa") && !User.IsInRole("Elf"))
            {
                query = query.Where(r => r.UserId == userId);
            }

            var requests = await query.ToListAsync();

            var viewModels = requests.Select(r => new GiftRequestViewModel
            {
                Id = r.Id,
                GiftName = r.Gift.GiftName,
                UserName = r.User.UserName,
                IsApproved = r.IsApproved
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            var gifts = await context.Gifts.Where(g => g.IsApproved).ToListAsync();
            ViewData["GiftId"] = new SelectList(gifts, "Id", "GiftName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiftRequestInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(User);
                var giftRequest = new GiftRequest
                {
                    GiftId = input.GiftId,
                    UserId = userId,
                    IsApproved = false
                };

                context.Add(giftRequest);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var gifts = await context.Gifts.Where(g => g.IsApproved).ToListAsync();
            ViewData["GiftId"] = new SelectList(gifts, "Id", "GiftName", input.GiftId);
            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await context.GiftRequests.FindAsync(id);
            if (request != null)
            {
                request.IsApproved = true;
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
