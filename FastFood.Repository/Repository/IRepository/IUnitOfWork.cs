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
        IItemImageRepository ItemImage {  get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
