using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Model;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductManagementDBContext _dbContext;

        public ProductController(ProductManagementDBContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _dbContext.Products;
        }

        [HttpGet("{productId:string}")]
        [Authorize]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            return product;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(Product product)
        {
            if (product == null)
            {
                return new StatusCodeResult(500);
            }

            IQueryable<Product> products = _dbContext.Products.AsQueryable().Where(x => x.ProductName == product.ProductName);
            if (products.Count() > 0)
            {
                product.ProductId = Guid.NewGuid().ToString();
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return new StatusCodeResult(500);
        }

        [HttpPut("{productId:string}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(string productId,Product product)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return new StatusCodeResult(500);
            }
            IQueryable<Product> products = _dbContext.Products.AsQueryable().Where(x => x.ProductId == productId);
            if(products.Count() > 1)
            {
                return new StatusCodeResult(500);
            }

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        //[HttpDelete("{productId:int}")]
        //public async Task<ActionResult> Delete(int productId)
        //{
        //    var product = await _dbContext.Products.FindAsync(productId);
        //    _dbContext.Products.Remove(product);
        //    await _dbContext.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
