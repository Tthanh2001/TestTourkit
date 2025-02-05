using Microsoft.AspNetCore.Mvc;
using WebTestTourkit.Models;

namespace WebTestTourkit.Controllers
{
    [Route("api/producttypes")]
    [ApiController]
    public class ProductTypeController : Controller
    {
        private readonly ProductManagementContext _context;
        public ProductTypeController(ProductManagementContext context)
        {
            _context = context;
        }

        // Get All ProductTypes
        [HttpGet]
        public IActionResult GetAll()
        {
            var productTypes = _context.ProductTypes
                .Select(pt => new
                {
                    pt.Id,
                    pt.Name,
                    ProductCount = _context.Product_ProductTypes.Count(p => p.ProductTypeId == pt.Id),
                    pt.EntryDate
                })
                .ToList();

            return Ok(productTypes);
        }

        // Add ProductType
        [HttpPost]
        public IActionResult Add(ProductType productType)
        {
            if (_context.ProductTypes.Any(pt => pt.Name == productType.Name))
                return BadRequest("ProductType name already exists.");

            if (productType.EntryDate == default)
                return BadRequest("Invalid entry date.");

            _context.ProductTypes.Add(productType);
            _context.SaveChanges();
            return Ok();
        }

        // Update ProductType
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductType productType)
        {
            var existingProductType = _context.ProductTypes.Find(id);
            if (existingProductType == null) return NotFound();

            existingProductType.Name = productType.Name;
            existingProductType.EntryDate = productType.EntryDate;

            _context.SaveChanges();
            return Ok();
        }

        // Delete ProductType
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productType = _context.ProductTypes.Find(id);
            if (productType == null) return NotFound();

            // Check if ProductType has associated products
            var productCount = _context.Product_ProductTypes.Count(pt => pt.ProductTypeId == id);
            if (productCount > 0)
            {
                return BadRequest("Cannot delete ProductType, it has associated products.");
            }

            _context.ProductTypes.Remove(productType);
            _context.SaveChanges();
            return Ok();
        }
    }
}
}
