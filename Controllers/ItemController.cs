using CodeZoneStoreTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneStoreTask.Controllers
{
    public class ItemController : Controller
    {
        private readonly storeContext _context;
        public ItemController(storeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.ToListAsync();
            return View(items);
        }
        [HttpPost]
        public async Task<IActionResult> Add(string itemName)
        {
            var check = _context.Items.FirstOrDefault(a => a.itemName == itemName);
            if (check == null)
            {
                Item it = new Item();
                it.itemName = itemName;
                _context.Add(it);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("itemName", "This Name has been added before");
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, string itemName)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                var check = _context.Items.FirstOrDefault(a => a.itemName == itemName);
                if (check == null)
                {
                    item.itemName = itemName;
                    _context.Update(item);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
