using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartInventory3.Data;
using SmartInventory3.Models;
using System.Linq;
using System;
using System.Collections.Generic;


namespace SmartInventory3.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Orders/Create
        public IActionResult Create()
        {
            // Prepare a list of products for the order form.
            ViewBag.Products = _context.Products.ToList();

            // Initialize OrderViewModel with one OrderItemViewModel per product.
            var orderVM = new OrderViewModel
            {
                OrderItems = _context.Products.Select(p => new OrderItemViewModel
                {
                    ProductId = p.Id,
                    Quantity = 0
                }).ToList()
            };

            return View(orderVM);
        }

        // Post: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    GuestName = orderVM.GuestName,
                    GuestEmail = orderVM.GuestEmail,
                    OrderItems = new List<OrderItem>()
                };

                decimal totalPrice = 0;
                foreach (var item in orderVM.OrderItems)
                {
                    if (item.Quantity > 0)
                    {
                        var product = await _context.Products.FindAsync(item.ProductId);
                        if (product != null)
                        {
                            order.OrderItems.Add(new OrderItem
                            {
                                ProductId = product.Id,
                                Quantity = item.Quantity
                            });
                            totalPrice += product.Price * item.Quantity;
                        }
                    }
                }
                order.TotalPrice = totalPrice;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("OrderSummary", new { id = order.Id });
            }
            ViewBag.Products = _context.Products.ToList();
            return View(orderVM);
        }

        // Orders/OrderSummary
        public async Task<IActionResult> OrderSummary(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound();
            return View(order);
        }
    }
}
