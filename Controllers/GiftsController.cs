using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab5_start.Data;
using lab5_start.Models;
using lab5_start.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace lab5_start.Controllers
{
    [Authorize]
    public class GiftsController(ApplicationDbContext context) : Controller {
        public async Task<IActionResult> Index()
        {
            var gifts = await context.Gifts.ToListAsync();
            var viewModels = gifts.Select(g => new GiftViewModel
            {
                Id = g.Id,
                GiftName = g.GiftName,
                IsApproved = g.IsApproved
            }).ToList();
            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var gift = await context.Gifts.FindAsync(id);
            if (gift == null) return NotFound();

            var viewModel = new GiftViewModel
            {
                Id = gift.Id,
                GiftName = gift.GiftName,
                IsApproved = gift.IsApproved
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Santa,Elf")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Santa,Elf")]
        public async Task<IActionResult> Create(GiftInputModel input)
        {
            if (ModelState.IsValid)
            {
                var gift = new Gift
                {
                    GiftName = input.GiftName,
                    IsApproved = User.IsInRole("Santa") ? input.IsApproved : false
                };
                context.Add(gift);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        [Authorize(Roles = "Santa,Elf")]
        public async Task<IActionResult> Edit(int id)
        {
            var gift = await context.Gifts.FindAsync(id);
            if (gift == null) return NotFound();

            var viewModel = new GiftViewModel
            {
                Id = gift.Id,
                GiftName = gift.GiftName,
                IsApproved = gift.IsApproved
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Santa,Elf")]
        public async Task<IActionResult> Edit(int id, GiftInputModel input)
        {
            if (ModelState.IsValid)
            {
                var gift = await context.Gifts.FindAsync(id);
                if (gift == null) return NotFound();

                gift.GiftName = input.GiftName;
                if (User.IsInRole("Santa"))
                {
                    gift.IsApproved = input.IsApproved;
                }

                context.Update(gift);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var viewModel = new GiftViewModel
            {
                Id = id,
                GiftName = input.GiftName,
                IsApproved = input.IsApproved
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Santa,Elf")]
        public async Task<IActionResult> Delete(int id)
        {
            var gift = await context.Gifts.FindAsync(id);
            if (gift == null) return NotFound();

            var viewModel = new GiftViewModel
            {
                Id = gift.Id,
                GiftName = gift.GiftName,
                IsApproved = gift.IsApproved
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Santa,Elf")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gift = await context.Gifts.FindAsync(id);
            if (gift != null)
            {
                context.Gifts.Remove(gift);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
