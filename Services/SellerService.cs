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

        ////Chamada síncrona:
        //public List<Seller> FindAll()
        //{
        //    return _context.Seller.ToList();
        //}

        //Chamada Assíncrona incluindo Task(async e await)
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        ////Chamada síncrona:
        //public void Insert (Seller obj)
        //{
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}

        //Chamada Assíncrona incluindo Task(async e await)
        public async Task InsertAsync(Seller obj)
        {
            //Add é feito em memória, não acessa o banco, então não precisa colocar assíncrono, colocar só no SaveChanges
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        ////Chamada síncrona:
        //public Seller FindById(int id)
        //{
        //    return _context.Seller.Include(o => o.Department).FirstOrDefault(o => o.Id == id);
        //}

        //Chamada Assíncrona incluindo Task(async e await)
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        ////Chamada síncrona:
        //public void Remove(int id)
        //{
        //    var o = _context.Seller.Find(id);
        //    _context.Seller.Remove(o);
        //    _context.SaveChanges();
        //}

        //Chamada Assíncrona incluindo Task(async e await)
        public async Task RemoveAsync(int id)
        {
            try
            {
                var o = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(o);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                //Mensagem interna do Entity:
                //throw new IntegrityException(e.Message);

                //Mensagem personalizada
                throw new IntegrityException("Cant delete seller because he/she has sales");

            }

        }

        ////Chamada síncrona:
        //public void Update(Seller o)
        //{
        //    if (!_context.Seller.Any(c => c.Id == o.Id))
        //    {
        //        throw new NotFoundException("Id not found");
        //    }
        //    try
        //    {
        //        _context.Update(o);
        //        _context.SaveChanges();
        //    }
        //     catch (DbUpdateConcurrencyException e)
        //    {
        //        throw new DbConcurrencyException(e.Message);
        //    }
        //}

        //Chamada Assíncrona incluindo Task(async e await)
        public async Task UpdateAsync(Seller o)
        {
            bool hasAny = await _context.Seller.AnyAsync(c => c.Id == o.Id);
            if (!hasAny)
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
