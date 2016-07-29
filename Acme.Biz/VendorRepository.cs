using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    public class VendorRepository
    {
        private List<Vendor> vendors;

        /// <summary>
        /// Retrieve one vendor.
        /// </summary>
        public Vendor Retrieve(int vendorId)
        {
            // Create the instance of the Vendor class
            Vendor vendor = new Vendor();

            // Code that retrieves the defined customer

            // Temporary hard coded values to return 
            if (vendorId == 1)
            {
                vendor.VendorId = 1;
                vendor.CompanyName = "ABC Corp";
                vendor.Email = "abc@abc.com";
            }
            return vendor;
        }

        /// <summary>
        /// Retrieves all of the approved vendors.
        /// </summary>
        //public Vendor[] RetrieveArray()
        //{
        //    var vendors = new Vendor[2]
        //    {
        //        new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" },
        //        new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" }
        //    };

        //    return vendors;
        //}

        /// <summary>
        /// Retrieves all of the approved vendors.
        /// By returning an interface we have a much more flexible result! We can keep the code more DRY
        /// With ICollection<T> we can add and remove elements, edits done to the collection will be reflected by the collection
        /// If we had IEnumerable<T> we can only iterate so any edits are not reflected by the collection
        /// </summary>
        public IEnumerable<Vendor> Retrieve()
        {
            if (vendors == null)
            {
                vendors = new List<Vendor>();

                vendors.Add(new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" });
                vendors.Add(new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" });
            }

            //iterates through every element. can change properties of the items in the list but cannot change the item itself
            foreach (Vendor vendor in vendors)
            {
                //Console.WriteLine(vendor);
            }

            //can iterate through in ways other than just every item--more flexibility. 
            for (int i = 0; i < vendors.Count; i++)
            {
                Console.WriteLine(vendors[i]);
            }

            return vendors;
        }

        /// <summary>
        /// Retrieves all of the vendors.
        /// </summary>
        public IEnumerable<Vendor> RetrieveAll()
        {
            vendors = new List<Vendor>()
            {
                { new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" }},
                { new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" }},
                { new Vendor() { VendorId = 3, CompanyName = "EFG Ltd", Email = "abc@abc.com" }},
                { new Vendor() { VendorId = 4, CompanyName = "HIJ AG", Email = "xyz@xyz.com" }},
                { new Vendor() { VendorId = 5, CompanyName = "Amalgamated Toys", Email = "abc@abc.com" }},
                { new Vendor() { VendorId = 6, CompanyName = "Toy Blocks Inc", Email = "xyz@xyz.com" }},
                { new Vendor() { VendorId = 7, CompanyName = "Home Products Inc", Email = "abc@abc.com" }},
                { new Vendor() { VendorId = 8, CompanyName = "Toys for Fun", Email = "xyz@xyz.com" }},
                { new Vendor() { VendorId = 9, CompanyName = "Car Toys", Email = "xyz@xyz.com" }}
            };

            return vendors;
        }

        /// <summary>
        /// Retrieves all of the approved vendors, one at a time
        /// The return type of an iterator method must be an IEnumerable type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vendor> RetrieveWithIterator()
        {
            //Get the data from the database
            this.Retrieve();

            foreach (var vendor in vendors)
            {
                Console.WriteLine($"Vendor ID: {vendor.VendorId}");
                //yield return results deferred execution and lazy evaluation
                //deferred execution means that the method containing the yield return is not executed until the collection is iterated via foreach, cast, or link
                //lazy evaluation means the method containing the yield return only returns one element at a time
                yield return vendor;
            }
        }

        /// <summary>
        /// Retrieves all of the approved vendors.
        /// </summary>
        //public Dictionary<string, Vendor> RetrieveWithKey()
        //{
        //    var vendors = new Dictionary<string, Vendor>()
        //    {
        //        { "ABC Corp", new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" }},
        //        { "XYZ Inc", new Vendor() { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" }}
        //    };

        //    //lookup item by key
        //    Console.WriteLine(vendors["XYZ Inc"]);

        //    //lookup item but first ensure that that key exists
        //    if (vendors.ContainsKey("XYZ"))
        //    {
        //        Console.WriteLine(vendors["XYZ"]);
        //    }

        //TryGetValue--declare a variable to be used -> out parameter is where the value is stored if the key is found
        //This way we do not have to look up by key twice, so it can be more efficient
        //Vendor vendor;
        //if (vendors.TryGetValue("XYZ", out vendor))
        //{
        //    Console.WriteLine(vendor);
        //}

        ////loop through vendor keys
        //foreach (var companyName in vendors.Keys)
        //{
        //    Console.WriteLine(vendors[companyName]);
        //}

        //iterate through values in the dictionary
        //foreach (var vendor in vendors.Values)
        //{
        //    Console.WriteLine(vendor);
        //}

        //iterate throught the elements in the dictionary (key-value pairs)
        //    foreach (var element in vendors)
        //    {
        //        var vendor = element.Value;
        //        var key = element.Key;
        //        Console.WriteLine($"Key: {key} Value: {vendor}");
        //    }

        //    return vendors;
        //}

        //example of a generic method
        public T RetrieveValue<T>(string sql, T defaultValue)
        {
            T value = defaultValue;

            return value;
        }

        public bool Save(Vendor vendor)
        {
            var success = true;

            // Code that saves the vendor

            return success;
        }
    }
}
