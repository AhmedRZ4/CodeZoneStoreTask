using CodeZoneStoreTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CodeZoneStoreTask.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly storeContext _context;
        public PurchaseController(storeContext context)
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
            var check = _context.Purchases.FirstOrDefault(p=>p.storeId==purch.storeId&&p.itemId==purch.itemId);
            
            if (check != null)
            {
                check.quantityP += purch.quantityP;
                _context.Update(check);
            }
            else
            {
                Purchase pu = new Purchase();
                pu.storeId = purch.storeId;
                pu.itemId = purch.itemId;
                pu.quantityP = purch.quantityP;
                _context.Add(pu);
            }                      
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
