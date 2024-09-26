using FastFood.Models;
using FastFood.Repository.Data;
using FastFood.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Repository.Repository
{
    internal class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly ApplicationDbContext _db;
        public ItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Item newItem)
        {

            Item itemFromDb = _db.Items.FirstOrDefault(u => u.Id == newItem.Id);
            
            itemFromDb.Title = newItem.Title;
            itemFromDb.Description = newItem.Description;
            itemFromDb.CategoryId = newItem.CategoryId;
            itemFromDb.SubCategoryId = newItem.SubCategoryId;
            itemFromDb.Price = newItem.Price;
            if (newItem.ImageUrl != null)
            {
                itemFromDb.ImageUrl = newItem.ImageUrl;
            }
        }
    }
}
