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
	public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public ISubCategoryRepository SubCategory { get; private set; } 
        public IItemRepository Item { get; private set; }

        public IItemImageRepository ItemImage { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
		public IOrderDetailRepository OrderDetail { get; private set; }

		private readonly ApplicationDbContext _db;

        public UnitOfWork( ApplicationDbContext db)
        {
            _db = db;

            Category = new CategoryRepository(_db);
            SubCategory = new SubCategoryRepository(_db);
            Item = new ItemRepository(_db);
            ItemImage = new ItemImageRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
			OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
