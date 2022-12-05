﻿using SalesWebMvc.Data;

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
}