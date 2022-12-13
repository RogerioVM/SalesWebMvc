﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models.ViewModels;

public class SalesRecordService
{
    private readonly SalesWebMvcContext _context;
    public SalesRecordService(SalesWebMvcContext context)
    {
        _context = context;
    }
    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from obj in _context.SalesRecord select obj; // retorna um objeto IQueryable do tipo DbSet(No caso DbSet<SalesRecord).
        if (minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }
        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value);
        }

        return await result
            .Include(x => x.Seller)
            .Include(x => x.Seller.Department)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }
    public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from obj in _context.SalesRecord select obj;
        if (minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }
        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value);
        }

        // Solução para consertar o erro desse endpoint devido a estarmos no .NET 6

        var data = await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

        return data.GroupBy(x => x.Seller.Department).ToList();
    }
}
