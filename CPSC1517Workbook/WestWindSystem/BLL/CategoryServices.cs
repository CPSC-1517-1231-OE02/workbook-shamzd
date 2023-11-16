using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    internal class CategoryServices
    {

        private readonly WestWindContext _context;

        internal CategoryServices(WestWindContext context) 
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList<Category>();
        }
    }
}
