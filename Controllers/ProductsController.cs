using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartInventory3.Data;
using SmartInventory3.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmartInventory3.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId, string sortOrder)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                products = products.Where(p => p.Name.Contains(searchString));

            if (categoryId.HasValue && categoryId.Value > 0)
                products = products.Where(p => p.CategoryId == categoryId.Value);

            products = sortOrder switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "name_asc" => products.OrderBy(p => p.Name),
                "name_desc" => products.OrderByDescending(p => p.Name),
                _ => products.OrderBy(p => p.Id)
            };

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View(await products.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(query) || p.Category.Name.Contains(query))
                .ToListAsync();

            return PartialView("_ProductListPartial", results);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,CategoryId,Price,Quantity,LowStockThreshold")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating product.");
                    ModelState.AddModelError("", "Unable to create product. Please try again later.");
                }
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,CategoryId,Price,Quantity,LowStockThreshold")] Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException dbEx)
                {
                    if (!_context.Products.Any(e => e.Id == product.Id))
                        return NotFound();
                    else
                    {
                        _logger.LogError(dbEx, "Concurrency error editing product with id {ProductId}", product.Id);
                        return RedirectToAction("Error", "Home");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error editing product with id {ProductId}", product.Id);
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                try
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting product with id {ProductId}", id);
                    return RedirectToAction("Error", "Home");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
