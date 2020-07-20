using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Web.Models;
using Inventory.Web.Services;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Web.Test.Services
{
    [TestFixture()]
    public class ProductViewModelFactoryTest
    {
        private Mock<IMediator> _mediator;
        private IProductViewModelFactory _factory;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _factory = new ProductViewModelFactory(_mediator.Object);
        }

        [Test]
        public async Task TestCreateShouldReturnProductViewModel()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetProductByIdQueryResponse()
            {
                Id = 2,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100,
                CreatedBy = "TestUser",
                CreatedDateTime = DateTimeOffset.Now
            });

            var result = await _factory.Create(2);

            Assert.That(result.GetType(), Is.EqualTo(typeof(ProductViewModel)));
        }


        [Test]
        public async Task TestExecuteSaveShouldReturnProductViewModelAndStringList()
        {
            ProductViewModel viewModel = new ProductViewModel {
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100,
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateProductCommandResponse()
            {
                Id = 1
            });

            var result = await _factory.ExceuteSave(viewModel);

            Assert.That(result.GetType(), Is.EqualTo(typeof((ProductViewModel Model, List<string> Errors))));
        }

        [Test]
        public async Task TestExecuteSaveShouldNotReturnErrors()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100,
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateProductCommandResponse()
            {
                Id = 1
            });

            var result = await _factory.ExceuteSave(viewModel);

            Assert.That(result.Errors.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task TestExecuteSaveShouldReturnErrors()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100,
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateProductCommandResponse()
            {
                Id = 0,
                ErrorMessage = "Name is exists in database"
            });

            var result = await _factory.ExceuteSave(viewModel);

            Assert.That(result.Errors.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task TestExecuteUpdateShouldReturnProductViewModelAndStringList()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100,
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UpdateProductCommandResponse()
            {
            });

            var result = await _factory.ExceuteUpdate(viewModel);

            Assert.That(result.GetType(), Is.EqualTo(typeof((ProductViewModel Model, List<string> Errors))));
        }
    }
}
