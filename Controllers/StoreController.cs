using CodeZoneStoreTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneStoreTask.Controllers
{
    public class StoreController : Controller
    {
        private readonly storeContext _context;
       public StoreController(storeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stores= await _context.Stores.ToListAsync();
            return View(stores);
        }
        [HttpPost]
        public async Task<IActionResult> Add(string storeName)
        {
            var check=_context.Stores.FirstOrDefault(a=>a.storeName==storeName);
            if (check==null)
            {              
                Store st = new Store();
                st.storeName = storeName;
                _context.Add(st);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("storeName", "This Name has been added before");
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id ,string storeName)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                var check = _context.Stores.FirstOrDefault(a => a.storeName == storeName);
                if (check == null)
                {
                    store.storeName = storeName;
                    _context.Update(store);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
    }                             
    }
}
