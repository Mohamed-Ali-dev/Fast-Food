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
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        private readonly ApplicationDbContext _db;
        public CouponRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(Coupon newcoupon)
        {
          
            _db.Update(newcoupon);
        }
    }
}
