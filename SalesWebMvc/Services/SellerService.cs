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

    public Seller FindById(int id) => _context.Seller.FirstOrDefault(x => x.Id == id);

    public void Remove(int id)
    {
        var obj = _context.Seller.Find(id);
        _context.Seller.Remove(obj);
        _context.SaveChanges();
    }
}
