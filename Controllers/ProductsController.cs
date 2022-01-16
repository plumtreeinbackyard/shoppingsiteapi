using shoppingsiteapi.Models;
using shoppingsiteapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace shoppingsiteapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // Get all products
        // GET: api/Products
        [HttpGet]
        public ActionResult<List<Product>> Get() =>
            _productService.Get();

        // Get one product
        // GET: api/Products/id
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // Add a new product to database
        // POST: api/Products
        [Authorize]
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        // Update a product
        // PUT: api/Products/id
        [Authorize]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product productIn)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();
        }

        // Delete a product
        // DELETE: api/Products/id
        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return NoContent();
        }

        // Update cart products' inventory
        [Authorize]
        [HttpPost("updateinventory")] 
        public IActionResult UpdateInventory(CartItem[] cartItems)
        {
            for (int i = 0; i < cartItems.Length; i++) {
                
                var product = _productService.Get(cartItems[i].Id);

                if (product == null)
                {
                    return NotFound();
                }
                var newInventory = product.Inventory - cartItems[i].Quantity;               
                _productService.UpdateInventory(cartItems[i].Id, newInventory);                
            }

            return NoContent();
        }
    }
}