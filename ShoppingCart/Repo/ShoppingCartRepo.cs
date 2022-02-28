using Microsoft.EntityFrameworkCore;
using ShoppingCart.Database;
using ShoppingCart.Model;
using ShoppingCart.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repo
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly DatabaseContext _context;
        public ShoppingCartRepo(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingVM> GetAllItems() => _context.ShoppingItems.ToList();

        public ShoppingVM GetById(Guid id) => _context.ShoppingItems.Find(id);

        public ShoppingVM Add(ShoppingVM newItem)
        {
            newItem.Id = Guid.NewGuid();
            _context.ShoppingItems.Add(newItem);
            _context.SaveChanges();

            return newItem;
        }

        public void Remove(ShoppingVM item)
        {
            _context.ShoppingItems.Remove(item);
            _context.SaveChanges();
        }
    }
}
