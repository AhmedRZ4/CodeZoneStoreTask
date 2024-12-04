using CodeZoneStoreTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneStoreTask.Controllers
{
    public class SellController : Controller
    {
        private readonly storeContext _context;
        public SellController(storeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = _context.Items.Select(i => new { i.itemId, i.itemName }).ToList();
            var stores = _context.Stores.Select(s => new { s.storeId, s.storeName }).ToList();
            ViewBag.Items = new SelectList(items, "itemId", "itemName");
            ViewBag.Stores = new SelectList(stores, "storeId", "storeName");
            var purchases = await _context.Purchases.ToListAsync();
            return View(purchases);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Purchase purch)
        {
            var check = _context.Purchases.FirstOrDefault(p => p.storeId == purch.storeId && p.itemId == purch.itemId);
            if (check != null)
            {
                if (check.quantityP >= purch.quantityP)
                {
                    check.quantityP -= purch.quantityP;
                    _context.Update(check);
                    var checksell = _context.Sells.FirstOrDefault(p => p.purchaseId == check.purchaseId);
                    if (checksell != null)
                    {
                        checksell.quantityS += purch.quantityP;
                    }
                    else
                    {
                        Sell se = new Sell();
                        se.quantityS = purch.quantityP;
                        se.purchaseId = check.purchaseId;
                        _context.Add(se);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
