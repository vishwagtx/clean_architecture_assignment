using Inventory.Web.Controllers;
using Inventory.Web.Services;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Inventory.Application.Queries;
using System.Threading;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Inventory.Web.Models;
using Inventory.Application.Commands;

namespace Inventory.Web.Test.Controllers
{
    [TestFixture()]
    public class ProductsControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IProductViewModelFactory> _productViewModelFactory;
        private Mock<IProductDetailViewModelFactory> _productDetailViewModelFactory;
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _productViewModelFactory = new Mock<IProductViewModelFactory>();
            _productDetailViewModelFactory = new Mock<IProductDetailViewModelFactory>();
            _controller = new ProductsController(_mediator.Object, _productViewModelFactory.Object, _productDetailViewModelFactory.Object);
        }

        [Test]
        public async Task TestIndexShouldReturnProductList()
        {
            List<ProductListDto> list = new List<ProductListDto>
              {
                  new ProductListDto{
                      Id = 1,
                      Name = "Test 1",
                      NoOfUnit = 4,
                      ReOrderLevel = 5,
                      UnitPrice = 100
                  },
                  new ProductListDto{
                      Id = 2,
                      Name = "Test 2",
                      NoOfUnit = 10,
                      ReOrderLevel = 5,
                      UnitPrice = 200
                  }
              };

            _mediator.Setup(m => m.Send(It.IsAny<GetProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetProductListQueryResult()
            {
                Products = list
            });

            var result = await _controller.Index() as ViewResult;
            Assert.That(result.Model, Is.EquivalentTo(list));
        }

        [Test]
        public async Task TestCreateShouldReturnId()
        {
            ProductViewModel product = new ProductViewModel
            {
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            };

            _productViewModelFactory.Setup(m => m.ExceuteSave(product))
            .ReturnsAsync((new ProductViewModel
            {
                Id = 2,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            }, new List<string> { }));

            var result = await _controller.Create(product) as ViewResult;

            Assert.That((result.Model as ProductViewModel).Id, Is.EqualTo(2));
        }

        [Test]
        public async Task TestCreateModelStateShouldInvalidWithErrors()
        {
            ProductViewModel product = new ProductViewModel
            {
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            };

            _productViewModelFactory.Setup(m => m.ExceuteSave(product))
            .ReturnsAsync((new ProductViewModel
            {
                Id = 0,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            }, new List<string> { "Name is exists in database" }));

            var result = await _controller.Create(product) as ViewResult;

            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result.ViewData.ModelState.ErrorCount, Is.EqualTo(1));
        }

        [Test]
        public async Task TestEditShouldNotReturnErrors()
        {
            ProductViewModel product = new ProductViewModel
            {
                Id = 1,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            };

            _productViewModelFactory.Setup(m => m.ExceuteUpdate(product))
            .ReturnsAsync((new ProductViewModel
            {
                Id = 1,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            }, new List<string> { }));

            var result = await _controller.Edit(product) as ViewResult;

            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(result.ViewData.ModelState.ErrorCount, Is.EqualTo(0));
        }

        [Test]
        public async Task TestDetailsShouldBeReturnProductDetailViewModel()
        {
            int id = 1;
            ProductDetailViewModel viewModel = new ProductDetailViewModel
            {
                Id = 1,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            };

            _productDetailViewModelFactory.Setup(m => m.Create(id))
            .ReturnsAsync(viewModel);

            var result = await _controller.Details(id) as ViewResult;

            Assert.That(result.ViewData.Model.GetType(), Is.EqualTo(typeof(ProductDetailViewModel)));
            Assert.That(result.ViewData.Model, Is.EqualTo(viewModel));
        }

        [Test]
        public async Task TestDeleteShouldRedirectToIndex()
        {
            int id = 1;
            ProductDetailViewModel viewModel = new ProductDetailViewModel
            {
                Id = id
            };

            _mediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(true);

            var result = await _controller.Delete(viewModel) as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }
    }
}
