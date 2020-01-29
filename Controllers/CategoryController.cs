using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Context contex;

        public CategoryController(Context contex)
        {
            this.contex = contex;
        }

        [Route("v1/categories")]
        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public IEnumerable<Category> Get()
        {
            return contex.Categories.AsNoTracking().ToList();
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category Get(int id)
        {
            return contex.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IEnumerable<Product> GetProducts(int id)
        {
            return contex.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
        }

        [Route("v1/categories")]
        [HttpPost]
        public Category Post([FromBody]Category category)
        {
            contex.Categories.Add(category);
            contex.SaveChanges();

            return category;
        }

        [Route("v1/categories")]
        [HttpPut]
        public Category Put([FromBody]Category category)
        {
            contex.Entry<Category>(category).State = EntityState.Modified;
            contex.SaveChanges();

            return category;
        }
    
        [Route("v1/categories")]
        [HttpDelete]
        public Category Delete([FromBody]Category category)
        {
            contex.Categories.Remove(category);
            contex.SaveChanges();

            return category;
        }
    }
}