using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Repository.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISubCategoryRepository SubCategory { get; }
        IItemRepository Item { get; }
        ICouponRepository Coupon {  get; }
        void Save();
    }
}
