using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Web.Models;
using Inventory.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _bus;
        private readonly IProductViewModelFactory _factory;
        private readonly IProductDetailViewModelFactory _readFactory;

        public ProductsController(IMediator bus, IProductViewModelFactory factory, IProductDetailViewModelFactory readFactory)
        {
            _bus = bus;
            _factory = factory;
            _readFactory = readFactory;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bus.Send(new GetProductListQuery());
            return View(result.Products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelResult = await _factory.ExceuteSave(model);
                model = modelResult.Model;

                if (modelResult.Errors.Count > 0)
                    AddErrors(modelResult.Errors);

                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _factory.Create(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelResult = await _factory.ExceuteUpdate(model);
                model = modelResult.Model;

                if (modelResult.Errors.Count > 0)
                    AddErrors(modelResult.Errors);

                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _readFactory.Create(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _readFactory.Create(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDetailViewModel model)
        {
            bool result = await _bus.Send(new DeleteProductCommand { Id = model.Id });

            if (result)
                return RedirectToAction("Index");
            else
                return View(model);
        }

        private void AddErrors(List<string> errors)
        {
            errors.ForEach(error =>
            {
                ModelState.AddModelError("", error);
            });
        }
    }
}