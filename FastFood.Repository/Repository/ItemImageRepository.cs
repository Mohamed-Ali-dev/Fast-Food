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
    public class ItemImageRepository : Repository<ItemImage>, IItemImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ItemImage itemImage)
        {
            _db.ItemImages.Update(itemImage);
        }
    }
}
