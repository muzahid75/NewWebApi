using Microsoft.AspNetCore.Mvc;
using NewWebApi.DB;
using NewWebApi.DB.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private NewDB _db;
        public ProductController(NewDB db)
        {
            _db = db;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var productList = _db.Products.ToList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var productObj = _db.Products.Find(id);
                if(productObj == null)
                {
                    return NotFound("Product Not Found");
                }
                return Ok(productObj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                _db.Products.Add(product);
                int result = _db.SaveChanges();

                if(result > 0)
                {
                    return Ok(new { product.Id });
                }
                return NotFound("Product not created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            try
            {
                // checking if product is available
                var productObj = _db.Products.Find(id);
                if (productObj == null)
                {
                    return NotFound("Product Not Found");
                }

                // trying to update the data
                productObj.Name = product.Name;
                productObj.Description = product.Description;
                _db.Products.Update(productObj);
                int result = _db.SaveChanges();

                if (result > 0)
                {
                    return Ok("Product updated");
                }
                return NotFound("Product not updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // checking if product is available
                var productObj = _db.Products.Find(id);
                if (productObj == null)
                {
                    return NotFound("Product Not Found");
                }

                // trying to delte the data
                _db.Products.Remove(productObj);
                int result = _db.SaveChanges();

                if (result > 0)
                {
                    return Ok("Product Deleted");
                }
                return NotFound("Product not deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
