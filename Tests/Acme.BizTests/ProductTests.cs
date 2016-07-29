using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        //Default Constructor -- setting properties
        [TestMethod()]
        public void SayHelloTest()
        {
            //Arrange -- set up the test: create an instance of the class using the "new" keyword and class name
            Product currentProduct = new Product();
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "15-inch steel blade hand saw";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";

            string expected = "Hello Saw (1): 15-inch steel blade hand saw Available on: ";

            //Act -- call the method being tested
            string actual = currentProduct.SayHello();

            //Asert -- compare
            Assert.AreEqual(expected, actual);
        }

        //Parameterized Constructor
        [TestMethod()]
        public void SayHello_ParameterizedConstructor()
        {
            //Arrange
            Product currentProduct = new Product(1, "Saw", "15-inch steel blade hand saw");

            string expected = "Hello Saw (1): 15-inch steel blade hand saw Available on: ";

            //Act
            string actual = currentProduct.SayHello();

            //Asert
            Assert.AreEqual(expected, actual);
        }

        //Object Initializer
        [TestMethod()]
        public void SayHelloTest_ObjectInitializer()
        {
            //Arrange
            Product currentProduct = new Product
            {
                ProductName = "Saw",
                ProductId = 1,
                Description = "15-inch steel blade hand saw"
            };

            string expected = "Hello Saw (1): 15-inch steel blade hand saw Available on: ";

            //Act
            string actual = currentProduct.SayHello();

            //Asert
            Assert.AreEqual(expected, actual);
        }

        //Null Check
        [TestMethod]
        public void Product_Null()
        {
            Product currentProduct = null;
            string companyName = currentProduct?.ProductVendor?.CompanyName;
            string expected = null;
            string actual = companyName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertMetersToInchesTest()
        {
            double expected = 78.74;
            double actual = 2 * Product.InchesPerMeter;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinimumPriceTest_Default()
        {
            Product currentProduct = new Product();
            decimal expected = .96m;
            decimal actual = currentProduct.MinimumPrice;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinimumPriceTest_Bulk()
        {
            Product currentProduct = new Product(1, "Bulk Tools", "");
            decimal expected = 9.99m;
            decimal actual = currentProduct.MinimumPrice;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductName_Format()
        {
            Product currentProduct = new Product();
            currentProduct.ProductName = "  Steel Hammer  ";
            string expected = "Steel Hammer";
            string actual = currentProduct.ProductName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductName_TooShort()
        {
            Product currentProduct = new Product();
            currentProduct.ProductName = "aw";
            string expected = null;
            string expectedMessage = "Product Name must be at least three characters.";

            string actual = currentProduct.ProductName;
            string actualMessage = currentProduct.ValidationMessage;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void ProductName_TooLong()
        {
            Product currentProduct = new Product();
            currentProduct.ProductName = "Product Name must be at least three characters";
            string expected = null;
            string expectedMessage = "Product Name cannot be more than twenty characters.";

            string actual = currentProduct.ProductName;
            string actualMessage = currentProduct.ValidationMessage;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void ProductName_JustRight()
        {
            Product currentProduct = new Product();
            currentProduct.ProductName = "Spam!";
            string expected = "Spam!";
            string expectedMessage = null;

            string actual = currentProduct.ProductName;
            string actualMessage = currentProduct.ValidationMessage;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void Category_DefaultValue()
        {
            Product currentProduct = new Product();
            string expected = "Tools";
            string actual = currentProduct.Category;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Category_NewValue()
        {
            Product currentProduct = new Product();
            currentProduct.Category = "Garden";
            string expected = "Garden";
            string actual = currentProduct.Category;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sequence_DefaultValue()
        {
            Product currentProduct = new Product();
            int expected = 1;
            int actual = currentProduct.SequenceNumber;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sequence_NewValue()
        {
            Product currentProduct = new Product();
            currentProduct.SequenceNumber = 5;
            int expected = 5;
            int actual = currentProduct.SequenceNumber;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductCode_DefaultValue()
        {
            Product currentProduct = new Product();
            string expected = "Tools-0001";
            string actual = currentProduct.ProductCode;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductCode_NewValue()
        {
            Product currentProduct = new Product();
            currentProduct.SequenceNumber = 5;
            currentProduct.Category = "Garden";
            string expected = "Garden-0005";
            string actual = currentProduct.ProductCode;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateSuggestedRetailTest()
        {            
            //Arrange
            Product currentProduct = new Product(1, "Saw", "");
            currentProduct.Cost = 50m;
            OperationResult<decimal> expected = new OperationResult<decimal>(55m, "");

            //Act
            OperationResult<decimal> actual = currentProduct.CalculateSuggestedRetail(10m);

            //Asert
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);

        }
    }
}