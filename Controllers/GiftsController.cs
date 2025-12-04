using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab5_start.Data;
using lab5_start.Models;
using lab5_start.Models.ViewModels;

namespace lab5_start.Controllers
{
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiftInputModel input)
        {
            if (ModelState.IsValid)
            {
                var gift = new Gift
                {
                    GiftName = input.GiftName,
                    IsApproved = input.IsApproved
                };
                context.Add(gift);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

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
        public async Task<IActionResult> Edit(int id, GiftInputModel input)
        {
            if (ModelState.IsValid)
            {
                var gift = await context.Gifts.FindAsync(id);
                if (gift == null) return NotFound();

                gift.GiftName = input.GiftName;
                gift.IsApproved = input.IsApproved;

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
