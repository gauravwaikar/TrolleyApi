using AutoFixture;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise2.Enums;
using TrolleyApi.Exercise2.Services;
using TrolleyApiTest.Common;
using Xunit;
using FluentAssertions;

namespace TrolleyApiTest.Exercise2
{
    public class PopularitySortServiceTest
    {
        private readonly PopularitySortService _sut;

        private readonly IFixture _fixture;

        private readonly IShoppingHistoryRepository _shopperHistoryRepository;

        public PopularitySortServiceTest()
        {
            _fixture = FixtureBuilder.Build();
            _shopperHistoryRepository = _fixture.Freeze<IShoppingHistoryRepository>();
            _sut = _fixture.Create<PopularitySortService>();
        }

        [Fact]
        public async Task Given_ShopperHistory_When_Sort_Returns_MostBoughtProductsFirst()
        {
            // Arrange.
            var shopperHistory = new List<ShopperHistoryResponse>
            {
                new ShopperHistoryResponse {
                    CustomerId = "1",
                    Products = new List<Product>
                    {
                        new Product { Name = "a", Quantity = 2d },
                        new Product { Name = "b", Quantity = 3d }
                    }
                },
                new ShopperHistoryResponse {
                    CustomerId = "2",
                    Products = new List<Product>
                    {
                        new Product { Name = "a", Quantity = 1d },
                        new Product { Name = "b", Quantity = 1d },
                        new Product { Name = "c", Quantity = 1d }
                    }
                },
            };

            _shopperHistoryRepository.Get(Arg.Any<string>()).Returns(shopperHistory);

            var productsToSort = new List<Product>
            {
                new Product { Name = "a" },
                new Product { Name = "b" },
                new Product { Name = "d" },
            };

            // Act.
            var result = await _sut.Sort(SortOptions.RECOMMENDED, productsToSort);

            // Assert.
            result[0].Should().Be(productsToSort[1]);
            result[1].Should().Be(productsToSort[0]);
            result[2].Should().Be(productsToSort[2]);
        }
    }
}
