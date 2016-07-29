using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor
    {
        //enums as alternatives to booleans
        public enum IncludeAddress { Yes, No };
        public enum SendCopy { Yes, No };
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        #region Method Chaining Example
        //Methods normally live below properties
        /// <summary>
        /// Sends a product order to a vendor
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of product to be ordered</param>
        /// <returns>Returns an OperationResult type (defined in Acme.Common\OperationResult.cs)</returns>
        //public OperationResult PlaceOrder(Product product, int quantity)
        //{
        //    //Method Chaining!
        //    return PlaceOrder(product, quantity, null, null);
        //}

        /// <summary>
        /// Sends a product order to a vendor
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of product to be ordered</param>
        /// <param name="deliverBy">Requested delivery date</param>
        /// <returns>Returns an OperationResult type (defined in Acme.Common\OperationResult.cs)</returns>
        //public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
        //{
        //    //Method Chaining!
        //    return PlaceOrder(product, quantity, deliverBy, null);
        //}

        /// <summary>
        /// Sends a product order to a vendor
        /// Example with optional parameters, without optional parameters, the preceding two commented out methods utilize method chaining)
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of product to be ordered</param>
        /// <param name="deliverBy">Requested delivery date</param>
        /// <param name="instructions">Delivery instructions.</param>
        /// <returns>Returns an OperationResult type (defined in Acme.Common\OperationResult.cs)</returns>
        #endregion
        public OperationResult<bool> PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy = null, string instructions = "standard delivery")
        {
            //guard clauses:
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            bool success = false;
            StringBuilder orderTextBuilder = new StringBuilder($"Order from Acme, Inc\r\nProduct: {product.ProductCode}\r\nQuantity: {quantity}");

            //nullable type properties being used
            if (deliverBy.HasValue)
                orderTextBuilder.Append($"\r\nDeliver By: {deliverBy.Value.ToString("d")}");
            if (!String.IsNullOrWhiteSpace(instructions))
                orderTextBuilder.Append($"\r\nInstructions: {instructions}");

            string orderText = orderTextBuilder.ToString();

            EmailService emailService = new EmailService();
            string confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent:"))
                success = true;

            //If we want to return both a success flag and other data, we can put this data in an object and return the entire object
            OperationResult<bool> operationResult = new OperationResult<bool>(success, orderText);
            return operationResult;
        }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="includeAddress">True to include the shipping address</param>
        /// <param name="sendCopy">True to send a copy of the email to the current customer</param>
        /// <returns>Success flag and order text</returns>
        public OperationResult<bool> PlaceOrder(Product product, int quantity, IncludeAddress includeAddress, SendCopy sendCopy)
        {
            string orderText = "Test";
            if (includeAddress == IncludeAddress.Yes) orderText += " With Address";
            if (sendCopy == SendCopy.Yes) orderText += " Wtih Copy";

            OperationResult<bool> operationResult = new OperationResult<bool>(true, orderText);
            return operationResult;
        }

        public override string ToString()
        {
            string vendorInfo = "Vendor: " + this.CompanyName;
            string result;
            result = vendorInfo?.ToLower();
            result = vendorInfo?.ToUpper();
            result = vendorInfo?.Replace("Vendor", "Supplier");

            int? length = vendorInfo?.Length;
            int? index = vendorInfo?.IndexOf(":");
            bool? begins = vendorInfo?.StartsWith("Vendor");

            return vendorInfo;
        }

        //override Equals
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Vendor compareVendor = obj as Vendor;
            if (compareVendor != null &&
                this.VendorId == compareVendor.VendorId &&
                this.CompanyName == compareVendor.CompanyName &&
                this.Email == compareVendor.Email)
                return true;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string PrepareDirections()
        {
            string directions = @"Insert \r\n to define a new line";
            return directions;
        }

        /// <summary>
        /// sends an email to a set of vendors
        /// </summary>
        /// <param name="vendors">Collection of vendors</param>
        /// <param name="message">Message to send</param>
        /// <returns></returns>
        public static List<string> SendEmail(ICollection<Vendor> vendors, string message)
        {
            var confirmations = new List<string>();
            var emailService = new EmailService();

            foreach (var vendor in vendors)
            {
                var subject = "Important message for: " + vendor.CompanyName;
                var confirmation = emailService.SendMessage(subject, message, vendor.Email);

                confirmations.Add(confirmation);
            }

            return confirmations;
        }

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message,
                                                        this.Email);
            return confirmation;
        }
    }
}
