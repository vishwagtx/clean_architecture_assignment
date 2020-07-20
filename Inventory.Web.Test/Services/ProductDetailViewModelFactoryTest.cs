using Inventory.Application.Queries;
using Inventory.Web.Models;
using Inventory.Web.Services;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Web.Test.Services
{
    [TestFixture()]
    public class ProductDetailViewModelFactoryTest
    {
        private Mock<IMediator> _mediator;
        private IProductDetailViewModelFactory _factory;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _factory = new ProductDetailViewModelFactory(_mediator.Object);
        }


        [Test]
        public async Task TestCreateShouldReturnProductDetailViewModel()
        {

            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetProductByIdQueryResponse()
            {
                Id = 1,
                Name = "Test",
                NoOfUnit = 12,
                ReOrderLevel = 1,
                UnitPrice = 100
            });

            var result = await _factory.Create(1);

            Assert.That(result.GetType(), Is.EqualTo(typeof(ProductDetailViewModel)));
        }
    }
}
