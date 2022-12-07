using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;

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
    public void Insert(Seller seller)
    {
        _context.Add(seller);
        _context.SaveChanges(); // Para inserir no banco de dados.
    }

    public Seller FindById(int id)
    {
        /* O "Include()" faz um join no DB e traz a informação desejada das tabelas que estão relacionadas,
        que no caso são as tabelas de vendedor (Seller) e de departamento (Departments). 
        Essa função pertence ao EF Core */

        return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);

    }

    public void Remove(int id)
    {
        var obj = _context.Seller.Find(id);
        _context.Seller.Remove(obj);
        _context.SaveChanges();
    }
    public void Update(Seller obj)
    {
        /*
            Aqui está sendo lançada uma exceção pela própria 
            camada de serviço pelo controlador, sem a necessidade
            do controlador ir até ao banco de dados.
            Considerando que a exceção "DbConcurrencyException" foi criada
            justamente pra isso como forma de explorar as características do MVC.
         */
        if (!_context.Seller.Any(x => x.Id == obj.Id))
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
        catch (DbConcurrencyException e) // exceção de concorrência do banco de dados (do entitty framework)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }
}
