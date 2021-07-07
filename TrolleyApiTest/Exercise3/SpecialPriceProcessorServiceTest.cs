using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrolleyApi.Exercise3.Domain;
using TrolleyApi.Exercise3.Services;
using TrolleyApiTest.Common;
using Xunit;

namespace TrolleyApiTest.Exercise3
{
    public class SpecialPriceProcessorServiceTest
    {
        private readonly IFixture _fixture;
        private readonly ISpecialPriceProcessorService  _sut;

        public SpecialPriceProcessorServiceTest()
        {
            _fixture = FixtureBuilder.Build();
            _sut = _fixture.Create<SpecialPriceProcessorService>();
        }

        [Fact]
        public void Given_Special_Products_List_Returns_Total_of_All_Special_Products_In_Trolley()
        {
            // Arrange.
            var specials = _fixture.CreateMany<Special>(2).ToList();
            var specialQuantityList1 = new List<SpecialQuantity>();
            SpecialQuantity specialQuantity = new SpecialQuantity();
            specialQuantity.Name = "Product A";
            specialQuantity.Quantity = 2;
            specialQuantityList1.Add(specialQuantity);

            specials[0].Quantities = specialQuantityList1;
            specials[0].Total = 10;

            var specialQuantityList2 = new List<SpecialQuantity>();
            SpecialQuantity specialQuantity2 = new SpecialQuantity();
            specialQuantity2.Name = "Product B";
            specialQuantity2.Quantity = 3;
            specialQuantityList2.Add(specialQuantity2);

            specials[1].Quantities = specialQuantityList2;
            specials[1].Total = 15;

            List<PurchasedQuantity> PurchaseQuantityList = new List<PurchasedQuantity>();            
            var purchasedQuantity = new PurchasedQuantity();
            purchasedQuantity.Name = "Product A";
            purchasedQuantity.Quantity = 2;
            PurchaseQuantityList.Add(purchasedQuantity);

            var purchasedQuantity2 = new PurchasedQuantity();
            purchasedQuantity2.Name = "Product B";
            purchasedQuantity2.Quantity = 3;
            PurchaseQuantityList.Add(purchasedQuantity2);

            var trolleyTotal = 0m;

            //Act
            foreach(var special in specials)
            {
                trolleyTotal += _sut.Calculate(special, PurchaseQuantityList);
            }
            //Assert
            trolleyTotal.Should().Be(25);
            
        }
        [Fact]
        public void Given_Special_Product_Quantity_has_special_price_other_at_normal_price()
        {
            //Arrange
            var special = new Special();
            var specialQuantityList = new List<SpecialQuantity>();
            var specialQuantity = new SpecialQuantity();
            specialQuantity.Name = "Product A";
            specialQuantity.Quantity = 5;
            specialQuantityList.Add(specialQuantity);
            special.Quantities = specialQuantityList;
            special.Total = 25;

            List<PurchasedQuantity> PurchaseQuantityList = new List<PurchasedQuantity>();
            var purchasedQuantity = new PurchasedQuantity();
            purchasedQuantity.Name = "Product A";
            purchasedQuantity.Quantity = 7;
            PurchaseQuantityList.Add(purchasedQuantity);

            //Act
            var trolleyTotal = 0m;
            trolleyTotal += _sut.Calculate(special, PurchaseQuantityList);

            //Assert
            trolleyTotal.Should().Be(25);
            PurchaseQuantityList[0].QuantityCalculatedForBill.Should().Be(5);
            PurchaseQuantityList[0].QuantityRemainingToBeBilled.Should().Be(2);
        }

        [Fact]
        public void Given_Special_Quantity_Higher_Than_Remaining_UnBilled_Trolley_Total_Is_Zero()
        {
            var special = new Special();
            var specialQuantityList = new List<SpecialQuantity>();
            var specialQuantity = new SpecialQuantity();
            specialQuantity.Name = "Product A";
            specialQuantity.Quantity = 5;
            specialQuantityList.Add(specialQuantity);
            special.Quantities = specialQuantityList;
            special.Total = 25;

            List<PurchasedQuantity> PurchaseQuantityList = new List<PurchasedQuantity>();
            var purchasedQuantity = new PurchasedQuantity();
            purchasedQuantity.Name = "Product A";
            purchasedQuantity.Quantity = 9;
            purchasedQuantity.MarkQuantityAsBilled(5);
            PurchaseQuantityList.Add(purchasedQuantity);

            //Act
            var trolleyTotal = 0m;
            trolleyTotal += _sut.Calculate(special, PurchaseQuantityList);

            //Assert
            trolleyTotal.Should().Be(0);
            
        }
    }
}
