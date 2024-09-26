using FastFood.Models;
using FastFood.Models.ViewModels;
using FastFood.Repository.Data;
using FastFood.Repository.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Repository.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CategoryVM categoryVM)
        {
            Category category = _db.Categories.Where(x => x.Id == categoryVM.Id).FirstOrDefault();
            category.Title = categoryVM.Title;
           
        }
    }
}
