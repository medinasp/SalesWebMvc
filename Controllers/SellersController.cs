using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        ////Operacao Sincrona
        //public IActionResult Index()
        //{
        //    var list = _sellerService.FindAll();
        //    return View(list);
        //}

        //Operacao Assincrona
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        ////Operacao Sincrona
        //public IActionResult Create()
        //{
        //    var departments = _departmentService.FindAll();
        //    var viewModel = new SellerFormViewModel { Departments = departments };
        //    return View(viewModel);
        //}

        //Operacao Assincrona
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        ////Operacao Sincrona
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create (Seller seller) 
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var departments = _departmentService.FindAll();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }
        //    _sellerService.Insert(seller);
        //    return RedirectToAction(nameof(Index));
        //}

        //Operacao Assincrona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        ////Operacao Sincrona
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }

        //    var obj = _sellerService.FindById(id.Value);

        //    if (obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }

        //    return View(obj);
        //}

        //Operacao Assincrona
        public async Task<IActionResult> Delete(int? id)
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

        ////Operacao Sincrona

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete (int id)
        //{
        //    _sellerService.Remove(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //Operacao Assincrona

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        ////Operacao Sincrona
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }

        //    var obj = _sellerService.FindById(id.Value);

        //    if (obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }

        //    return View(obj);
        //}

        //Operacao Assincrona
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

        ////Operacao Sincrona
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }

        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }

        //    List<Department> departments = _departmentService.FindAll();
        //    SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
        //    return View(viewModel);
        //}

        //Operacao Assincrona
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

        ////Operacao Sincrona
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Seller seller)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var departments = _departmentService.FindAll();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }

        //    if (id != seller.Id)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
        //    }
        //    try
        //    {
        //        _sellerService.Update(seller);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    //catch(NotFoundException e)
        //    //{
        //    //    return RedirectToAction(nameof(Error), new { message = e.Message });
        //    //}
        //    //catch (DbConcurrencyException e)
        //    //{
        //    //    return RedirectToAction(nameof(Error), new { message = e.Message });
        //    //}

        //    //Como os 2 catch acima fazem parte do ApplicationException, podemos juntar os 2 erros em um único catch:

        //    catch (ApplicationException e)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = e.Message });
        //    }
        //}

        //Operacao Assincrona
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }

            //catch(NotFoundException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
            //catch (DbConcurrencyException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}

            //Como os 2 catch acima fazem parte do ApplicationException, podemos juntar os 2 erros em um único catch:

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                //abaixo comando para pegar o Id interno da requisição
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
        }
    }
}
