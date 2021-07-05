using AutoFixture;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.ApiProxies;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise2.Enums;
using TrolleyApi.Exercise2.Services;
using TrolleyApiTest.Common;
using Xunit;

namespace TrolleyApiTest.Exercise2
{
    public class SortServiceTest
    {
        private readonly ISortService _sut;
        private readonly IFixture _fixture;

        private readonly IProductSortService _productSort1;
        private readonly IProductSortService _productSort2;
        private readonly IProductsRepository _productsRepository;

        public SortServiceTest()
        {
            _fixture = FixtureBuilder.Build();

            _productSort1 = _fixture.Create<IProductSortService>();
            _productSort2 = _fixture.Create<IProductSortService>();

            _fixture.Inject<IReadOnlyList<IProductSortService>>(new List<IProductSortService>
            {
                _productSort1,
                _productSort2
            });

            _productsRepository = _fixture.Freeze<IProductsRepository>();
            _sut = _fixture.Create<SortService>();
        }

        [Fact]
        public async Task Given_ValidInputs_When_Sort_Returns_SortedProducts()
        {
            // Arrange.
            _productSort1.SupportedSortOptions.Returns(new List<SortOptions> { SortOptions.ASCENDING, SortOptions.DESCENDING });
            _productSort2.SupportedSortOptions.Returns(new List<SortOptions> { SortOptions.HIGH, SortOptions.LOW });

            var products = _fixture.CreateMany<Product>(5).ToList();

            _productsRepository.Get(Arg.Any<string>()).Returns(products);

            _productSort1.Sort(Arg.Any<SortOptions>(), Arg.Is<List<Product>>(products)).Returns(products);

            // Act.
            var result = await _sut.Sort("ASCENDING");

            // Assert.
            result.SequenceEqual(products);
        }
    }
}
