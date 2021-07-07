using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise3.Domain;
using TrolleyApi.Exercise3.Services;
using TrolleyApiTest.Common;
using Xunit;

namespace TrolleyApiTest.Exercise3
{
    public class NormalPriceProcessorTest
    {
        private readonly IFixture _fixture;
        private readonly INormalPriceProcessorService _sut;

        public NormalPriceProcessorTest()
        {
            _fixture = FixtureBuilder.Build();
            _sut = _fixture.Create<NormalPriceProcessorService>();
        }

        [Fact]
        public void Given_Only_Normal_products_In_Trolley_Calculate_Total()
        {
            //Arrange            
            var purchasedQuantity = new PurchasedQuantity();
            purchasedQuantity.Name = "Product A";
            purchasedQuantity.Quantity = 10;            

            var productList = new List<Product>();
            var product = new Product();
            product.Name = "Product A";
            product.Price = 10;
            product.Quantity = 20;

            productList.Add(product);

            //Assert
            var trolleyTotal = 0m;
            trolleyTotal = _sut.Calculate(purchasedQuantity, productList);

            //Assert
            trolleyTotal.Should().Be(100);
        }
        
    }
}
