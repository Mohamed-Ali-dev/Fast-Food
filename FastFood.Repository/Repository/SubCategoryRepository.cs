using FastFood.Models;
using FastFood.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Repository.Repository.IRepository
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public SubCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

 

        public void Update(SubCategory subCategory)
        {
            _db.Update(subCategory);
        }
    }
}
