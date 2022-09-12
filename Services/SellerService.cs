using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert (Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(o => o.Department).FirstOrDefault(o => o.Id == id);
        }

        public void Remove(int id)
        {
            var o = _context.Seller.Find(id);
            _context.Seller.Remove(o);
            _context.SaveChanges();
        }

        public void Update(Seller o)
        {
            if (!_context.Seller.Any(c => c.Id == o.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(o);
                _context.SaveChanges();
            }
             catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
