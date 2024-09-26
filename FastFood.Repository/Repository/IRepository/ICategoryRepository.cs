using FastFood.Models;
using FastFood.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Repository.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(CategoryVM categoryVM);
    }
}
