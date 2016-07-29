using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorRepositoryTests
    {
        [TestMethod()]
        public void RetrieveValueTest()
        {
            VendorRepository repository = new VendorRepository();
            int expected = 42;

            int actual = repository.RetrieveValue<int>("Select...", 42);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RetrieveValueStringTest()
        {
            VendorRepository repository = new VendorRepository();
            string expected = "test";

            string actual = repository.RetrieveValue<string>("Select...", "test");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RetrieveValueObjectTest()
        {
            VendorRepository repository = new VendorRepository();
            Vendor vendor = new Vendor();
            Vendor expected = vendor;

            Vendor actual = repository.RetrieveValue<Vendor>("Select...", vendor);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RetrieveTest()
        {
            VendorRepository repository = new VendorRepository();

            var expected = new List<Vendor>();

            expected.Add(new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" });
            expected.Add(new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" });

            var actual = repository.Retrieve();

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void RetrieveWithIteratorTest()
        {
            var repository = new VendorRepository();
            var expected = new List<Vendor>()
            {
                { new Vendor() {VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" } },
                {new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" } }
            };

            var vendorIterator = repository.RetrieveWithIterator();

            foreach (var item in vendorIterator)
            {
                Console.WriteLine(item);
            }

            var actual = vendorIterator.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RetrieveAllTest()
        {
            var repository = new VendorRepository();
            var expected = new List<Vendor>()
            {
                { new Vendor() { VendorId = 5, CompanyName = "Amalgamated Toys", Email = "abc@abc.com" }},
                { new Vendor() { VendorId = 9, CompanyName = "Car Toys", Email = "xyz@xyz.com" }},
                { new Vendor() { VendorId = 6, CompanyName = "Toy Blocks Inc", Email = "xyz@xyz.com" }},
                { new Vendor() { VendorId = 8, CompanyName = "Toys for Fun", Email = "xyz@xyz.com" }}
            };

            var vendors = repository.RetrieveAll();

            //LINQ Query Syntax
            //var vendorQuery = from v in vendors
            //                  where v.CompanyName.Contains("Toy")
            //                  orderby v.CompanyName
            //                  select v;

            //LINQ Method Syntax using Lambda expression instead of a delegate
            var vendorQuery = vendors.Where(v => v.CompanyName.Contains("Toy")).OrderBy(v => v.CompanyName);

            CollectionAssert.AreEqual(expected, vendorQuery.ToList());
        }

        //[TestMethod()]
        //public void RetrieveWithKeyTest()
        //{
        //    VendorRepository repository = new VendorRepository();

        //    //Collection initializer syntax
        //    var expected = new Dictionary<string, Vendor>()
        //    {
        //        {"ABC Corp", new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" } },
        //        {"XYZ Inc",  new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" } }
        //    };

        //    Dictionary<string, Vendor> actual = repository.RetrieveWithKey();

        //    CollectionAssert.AreEqual(expected, actual);
        //}
    }
}