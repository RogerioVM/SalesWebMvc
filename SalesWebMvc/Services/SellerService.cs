using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;

public class SellerService
{
    private readonly SalesWebMvcContext _context;
    public SellerService(SalesWebMvcContext context)
    {
        _context = context;
    }
    public async Task<List<Seller>> FindAllAsync()
    {
        return await _context.Seller.ToListAsync();
    }
    public async Task InsertAsync(Seller seller)
    {
        _context.Add(seller);
        await _context.SaveChangesAsync(); // Para inserir no banco de dados.
    }

    public async Task<Seller> FindByIdAsync(int id)
    {
        /* O "Include()" faz um join no DB e traz a informação desejada das tabelas que estão relacionadas,
        que no caso são as tabelas de vendedor (Seller) e de departamento (Departments). 
        Essa função pertence ao EF Core */

        return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task RemoveAsync(int id)
    {
        try
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new IntegrityException(e.Message);
        }
    }
    public async Task UpdateAsync(Seller obj)
    {
        /*
            Aqui está sendo lançada uma exceção pela própria 
            camada de serviço pelo controlador, sem a necessidade
            do controlador ir até ao banco de dados.
            Considerando que a exceção "DbConcurrencyException" foi criada
            justamente pra isso como forma de explorar as características do MVC.
         */
        bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
        }
        catch (DbConcurrencyException e) // exceção de concorrência do banco de dados (do entitty framework)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }
}
