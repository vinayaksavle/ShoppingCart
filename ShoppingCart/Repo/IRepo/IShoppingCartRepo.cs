using ShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repo.IRepo
{
    public interface IShoppingCartRepo
    {
        IEnumerable<ShoppingVM> GetAllItems();
        ShoppingVM GetById(Guid id);
        ShoppingVM Add(ShoppingVM newItem);
        void Remove(ShoppingVM item);
    }
}
