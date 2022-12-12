using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models.ViewModels;

public class DepartmentService
{
    private readonly SalesWebMvcContext _context;
    public DepartmentService(SalesWebMvcContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> FindAllAsync()
    {
        return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // Usando LINQ pra retornar por nome
    }
}
