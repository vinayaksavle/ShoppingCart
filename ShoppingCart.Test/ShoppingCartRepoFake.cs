using ShoppingCart.Model;
using ShoppingCart.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Test
{
    public class ShoppingCartRepoFake : IShoppingCartRepo
    {
        private readonly List<ShoppingVM> _shoppingItems;

        public ShoppingCartRepoFake()
        {
            _shoppingItems = new List<ShoppingVM>()
            {
                new ShoppingVM() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                new ShoppingVM() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Dairy Milk", Manufacturer="Mad Cow", Price = 4.00M },
                new ShoppingVM() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza", Manufacturer="Uncle Mickey's", Price = 12.00M }
            };
        }

        public IEnumerable<ShoppingVM> GetAllItems()
        {
            return _shoppingItems;
        }

        public ShoppingVM GetById(Guid id)
        {
            return _shoppingItems.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public ShoppingVM Add(ShoppingVM newItem)
        {
            newItem.Id = Guid.NewGuid();
            _shoppingItems.Add(newItem);
            return newItem;
        }

        public void Remove(ShoppingVM item)
        {
            var existing = _shoppingItems.First(a => a.Id == item.Id);
            _shoppingItems.Remove(existing);
        }
    }
}
