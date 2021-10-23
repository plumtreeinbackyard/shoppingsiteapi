using shoppingsiteapi.Models;
using shoppingsiteapi.Services;
using System;
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

        // GET: api/Products
        [HttpGet]
        public ActionResult<List<Product>> Get() =>
            _productService.Get();

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

        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        // PUT: api/Products/id
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

        // DELETE: api/Products/id
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

        [HttpPut("updateinventory")] 
        public IActionResult UpdateInventory(CartItem[] cartItems)
        {
            // Console.WriteLine(cartItems[0]);
            // return NoContent();
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