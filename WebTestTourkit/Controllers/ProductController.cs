using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTestTourkit.Models;

namespace WebTestTourkit.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductManagementContext _context;
        public ProductController(ProductManagementContext context)
        {
            _context = context;
        }
        // Get All Products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products
                .Include(p => p.ProductTypes)  // Include the ProductTypes collection for each Product
                .ToList();

            return Ok(products);
        }



        // Add Product
        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (_context.Products.Any(p => p.Name == product.Name))
                return BadRequest("Product name already exists.");

            if (product.EntryDate == default)
                return BadRequest("Invalid entry date.");

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }

        // Update Product
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.EntryDate = product.EntryDate;

            _context.SaveChanges();
            return Ok();
        }

        // Delete Product
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}

