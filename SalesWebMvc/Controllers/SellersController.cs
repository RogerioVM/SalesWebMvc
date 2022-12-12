using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Diagnostics;

public class SellersController : Controller
{
    private readonly SellerService _sellerService;
    private readonly DepartmentService _departmentService;

    public SellersController(SellerService sellerService, DepartmentService departmentService)
    {
        _sellerService = sellerService;
        _departmentService = departmentService;
    }
    public async Task<IActionResult> Index()
    {
        var list = await _sellerService.FindAllAsync();
        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        var departments = await _departmentService.FindAllAsync();
        var viewModel = new SellerFormViewModel { Departments = departments };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // Pra previnir ataques CSRF
    public async Task<IActionResult> Create(Seller seller)
    {
        //if (!ModelState.IsValid) // Testar se o modelo foi validado.
        //{
        //    var departments = await _departmentService.FindAllAsync();
        //    var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };

        //    return View(viewModel);
        //}
        await _sellerService.InsertAsync(seller);
        return RedirectToAction(nameof(Index)); // Método pra redirecionar pra ação Index.
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        var obj = await _sellerService.FindByIdAsync(id.Value); // Por ser opcional, precisa usar o "Value"
        if (obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Delete(int id)
    {
        await _sellerService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        var obj = await _sellerService.FindByIdAsync(id.Value);
        if (obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }

        return View(obj);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        var obj = await _sellerService.FindByIdAsync(id.Value);
        if (obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }

        List<Department> departments = await _departmentService.FindAllAsync();
        SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Edit(int id, Seller seller)
    {

        //if (!ModelState.IsValid) // Testar se o modelo foi validado.
        //{
        //    var departments = await _departmentService.FindAllAsync();
        //    var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };

        //    return View(viewModel);
        //}

        if (id != seller.Id)
        {
            return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
        }

        try
        {
            await _sellerService.UpdateAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException e) // Pelo upcasting, o "ApplicationException" serve para as exceções personalizadas feitas.
        {

            return RedirectToAction(nameof(Error), new { message = e.Message });
        }
        
    }

    public IActionResult Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };

        return View(viewModel);
    }
}
