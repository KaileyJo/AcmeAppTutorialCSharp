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
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");
            OperationResult<bool> expected = new OperationResult<bool>(true, "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12\r\nInstructions: standard delivery");

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(product, 12);

            // Assert
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrder_ThreeParameters()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");
            OperationResult<bool> expected = new OperationResult<bool>(true, "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12\r\nDeliver By: 10/25/2016\r\nInstructions: standard delivery");

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(product, 12, new DateTimeOffset(2016, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            // Assert
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrder_ThreeParameters_NoDeliveryDate()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");
            OperationResult<bool> expected = new OperationResult<bool>(true, "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12\r\nInstructions: door 6");

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(product, 12, instructions: "door 6");

            // Assert
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrder_FourParameters()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");
            OperationResult<bool> expected = new OperationResult<bool>(true, "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12\r\n" +
                "Deliver By: 10/25/2016\r\nInstructions: door 6");

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(product, 12, new DateTimeOffset(2016, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)), "door 6");

            // Assert
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        //test guard clauses
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_NullProduct_Exception()
        {
            // Arrange
            Vendor vendor = new Vendor();

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(null, 12);

            // Assert
            // Expected exception
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrder_QuantityOutOfRange_Exception()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(product, -12);

            // Assert
            // Expected exception
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrder_DateOutOfRange_Exception()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");

            // Act
            OperationResult<bool> actual = vendor.PlaceOrder(product, 12, new DateTimeOffset(2015, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            // Assert
            // Expected exception
        }

        [TestMethod()]
        public void PlaceorderTest_WithAddress()
        {
            // Arrange
            Vendor vendor = new Vendor();
            Product product = new Product(1, "Saw", "");
            OperationResult<bool> expected = new OperationResult<bool>(true, "Test With Address");

            // Act
            //Example of named parameters
            //OperationResult actual = vendor.PlaceOrder(product, quantity: 12, includeAddress: true, sendCopy: false);

            //Alternative with enums instead of booleans
            OperationResult<bool> actual = vendor.PlaceOrder(product, 12, Vendor.IncludeAddress.Yes, Vendor.SendCopy.No);

            // Assert
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Vendor vendor = new Vendor();
            vendor.VendorId = 1;
            vendor.CompanyName = "ABC Corp";
            string expected = "Vendor: ABC Corp";
            string actual = vendor.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PrepareDirectionsTest()
        {
            Vendor vendor = new Vendor();
            string expected = @"Insert \r\n to define a new line";
            string actual = vendor.PrepareDirections();
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            var vendorRepository = new VendorRepository();
            var vendorsCollection = vendorRepository.Retrieve();
            var expected = new List<string>()
            {
                "Message sent: Important message for: ABC Corp",
                "Message sent: Important message for: XYZ Inc"
            };

            //casting the result to a list
            var vendors = vendorsCollection.ToList();

            Console.WriteLine(vendors.Count);

            var actual = Vendor.SendEmail(vendors, "Test Message");

            CollectionAssert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //public void SendEmailTestAdd()
        //{
        //    var vendorRepository = new VendorRepository();
        //    var vendorsCollection = vendorRepository.Retrieve();

        //    //this adds to our master list, it works with ICollection<T> but not IEnumerable<T>
        //    vendorsCollection.Add(new Vendor() { VendorId = 7, CompanyName = "EFG Ltd", Email = "efg@efg.com" });

        //    var vendorsMaster = vendorRepository.Retrieve();

        //    var expected = new List<string>()
        //    {
        //        "Message sent: Important message for: ABC Corp",
        //        "Message sent: Important message for: XYZ Inc"
        //    };

        //    //casting the result to a list
        //    var vendors = vendorsCollection.ToList();

        //    Console.WriteLine(vendors.Count);

        //    var actual = Vendor.SendEmail(vendors, "Test Message");

        //    CollectionAssert.AreEqual(expected, actual);
        //}

        [TestMethod()]
        public void SendEmailTestArray()
        {
            var vendorRepository = new VendorRepository();
            var vendorsCollection = vendorRepository.Retrieve();
            var expected = new List<string>()
            {
                "Message sent: Important message for: ABC Corp",
                "Message sent: Important message for: XYZ Inc"
            };

            //casting the result to an array
            var vendors = vendorsCollection.ToArray();
            Console.WriteLine(vendors.Length);

            var actual = Vendor.SendEmail(vendors, "Test Message");

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendEmailTestDictionary()
        {
            var vendorRepository = new VendorRepository();
            var vendorsCollection = vendorRepository.Retrieve();
            var expected = new List<string>()
            {
                "Message sent: Important message for: ABC Corp",
                "Message sent: Important message for: XYZ Inc"
            };

            //casting the result to a dictionary (setting the key to CompanyName)
            var vendors = vendorsCollection.ToDictionary(v => v.CompanyName);
            
            Console.WriteLine(vendors.Count);

            var actual = Vendor.SendEmail(vendors.Values, "Test Message");

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}