using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModel;
namespace ProductCatalog.Repositories
{
    public class ProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context context)
        {
            this.context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
           return context.Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .ToList();
        }

        public Product Get(int id)
        {
            return context.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        public void Save(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Entry<Product>(product).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}