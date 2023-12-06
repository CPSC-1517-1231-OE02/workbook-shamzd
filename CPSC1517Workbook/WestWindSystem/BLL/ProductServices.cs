using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext context)
        {
            _context = context;
        }

        public List<Product> GetProductsByCategoryId(int id) 
        {
            return _context.Products.Where( p=> p.CategoryId == id).Include(p=>p.Supplier).ToList<Product>();
        }

        public List<Product> GetProductsByNameOrCategoryName(string partial)
        {
            //Validation to make sure the field is not null when asking for the partial search
            //if (string.IsNullOrWhiteSpace(partial))
            if(partial == null)
            {
                throw new ArgumentNullException("Partial argument cannot be null", new ArgumentException());
            }
            partial = partial.ToLower();
            return _context.Products.Where(p => p.ProductName.ToLower().Contains(partial) || p.Category.CategoryName.ToLower().Contains(partial)).Include(p => p.Supplier).ToList<Product>();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }

        //method to add a new product to the DB
        public void AddProduct(Product product)
        {
            //check to make sure the product is not null
			if (product == null)
			{
				throw new ArgumentNullException("Product argument cannot be null", new ArgumentException());
			}
            //this is used to add the product to the DB
            _context.Products.Add(product);
            _context.SaveChanges();//this whill then save the changes to the DB
		}

        //Method to update the product
		public void UpdateProduct(Product product)
		{
            //check to make sure the product is not null
			if (product == null)
			{
				throw new ArgumentNullException("Product argument cannot be null", new ArgumentException());
			}

			_context.Products.Update(product);//this will update the product to the DB
			_context.SaveChanges();//this will save all the changes to the DB
		}

        //Method to discontinue the product in the Db
        public void DiscontinueProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product argument cannot be null", new ArgumentException());
            }

            product.Discontinued = true;//this is the bool value when it is set to true the DB will be switched to true 
            UpdateProduct(product);//You then update the product not save the changes... the UpdateProduct method is called and will save it then
        }
	}
}
