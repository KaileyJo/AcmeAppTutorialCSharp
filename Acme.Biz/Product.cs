using Acme.Common;
//This using statement allows the static members of the static LoggingService class to be used without specifying the namespace
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        //example of a constant usually defined above a constructor
        public const double InchesPerMeter = 39.37;
        //example of a readonly usually defined above a constructor
        //if this readonly was static it could only be initialized in a static constructor or on the declaration
        public readonly decimal MinimumPrice;

        #region Constructors
        //Default parameterless constructor created using "ctor" code snippet
        public Product()
        {
            var states = new Dictionary<string, string>()
            {
                {"CA", "California" },
                {"MN", "Minnesota" },
                { "TX", "Texas" }
            };
            Console.WriteLine(states);

            Console.WriteLine("Product instace created");
            //Creating and instance of Vendor here ensures that it is created whether the default or parameterized constructor is used
            //this.ProductVendor = new Vendor();

            //readonly initialization
            this.MinimumPrice = .96m;
            this.Category = "Tools";

            //Example using Generic List
            List<string> colorOptions = new List<string>() { "Red", "Espresso", "White", "Navy" };
            colorOptions.Insert(2, "Purple");
            colorOptions.Remove("White");
            Console.WriteLine(colorOptions);

            //Example of Array
            //string[] colorOptions = { "Red", "Espresso", "White", "Navy" };

            //int brownIndex = Array.IndexOf(colorOptions, "Espresso");

            //colorOptions.SetValue("Blue", 3);

            //for(int i = 0; i < colorOptions.Length; i++)
            //{
            //    colorOptions[i] = colorOptions[i].ToLower();
            //}

            //foreach (string color in colorOptions)
            //{
            //    Console.WriteLine($"Colors::: {color}!");
            //}
        }
        //Parameterized constructor. "this" invokes the parameterless constructor and executes that functionality first
        public Product(int productId, string productName, string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;

            //readonly alternative initialization
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine("Product instance has a name: " + ProductName);
        }
        #endregion

        #region Properties
        //Field. camelCase
        private string productName;
        //Property. PascalCase
        public string ProductName
        {
            get
            {
                string formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                    ValidationMessage = "Product Name must be at least three characters.";
                else if (value.Length > 20)
                    ValidationMessage = "Product Name cannot be more than twenty characters.";
                else
                    productName = value;
            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get
            {
                //lazy loading. Only instantiate the Vendor object if it has not yet been done, use this if the object is only needed occassionally
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        public string ValidationMessage { get; private set; }

        //auto-implemented property declared in a constructor
        internal string Category { get; set; }
        //auto-implemented property declared inline
        public int SequenceNumber { get; set; } = 1;

        //Lambda (Expression-Bodied Property) This is a read only property that immediately returns a value
        //Format a number to four places
        public string ProductCode => $"{this.Category}-{this.SequenceNumber:0000}";

        public decimal Cost { get; set; }
        #endregion

        /// <summary>
        /// Calculates suggested retail price (**Shocking, I know**)
        /// Example of a Lambda method (i.e. expression-bodied method)
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up cost.</param>
        /// <returns></returns>
        //public decimal CalculateSuggestedRetail(decimal markupPercent) =>
        //    this.Cost + (this.Cost * markupPercent / 100);
        public OperationResult<decimal> CalculateSuggestedRetail(decimal markupPercent)
        {
            string message = "";
            if (markupPercent <= 0)
                message = "Invalid markup percentage";
            else if (markupPercent < 10)
                message = "Below recommended markup percentage";

            decimal value = this.Cost + (this.Cost * markupPercent / 100);

            OperationResult<decimal> operationResult = new OperationResult<decimal>(value, message);

            return operationResult;
        }

        public string SayHello()
        {
            //Referencing class in the same namespace
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Product");

            //Referencing class in a different namespace -- requires adding a using statement
            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");

            //Referencing a static class -- notice there is no "new" keyword
            var result = LogAction("saying hello");

            return "Hello " + ProductName + " (" + ProductId + "): " + Description + " Available on: " + AvailabilityDate?.ToShortDateString();
        }

        //example of an override of the ToString method, this gives us more useful information when debugging things with the Product type
        public override string ToString()
        {
            return this.ProductName + " (" + this.ProductId + ")";
        }
    }
}
