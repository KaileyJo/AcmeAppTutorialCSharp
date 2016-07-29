namespace Acme.Common
{
    /// <summary>
    /// Provides a success flag and message 
    /// useful as a method return type.
    /// <T> is a generic type parameter
    /// </summary>
    public class OperationResult<T>
    {
        public OperationResult()
        {
        }

        public OperationResult(T result, string message) : this()
        {
            this.Result = result;
            this.Message = message;
        }

        //T is a generic type variable
        public T Result { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Provides a decimal amount and message 
    /// useful as a method return type.
    /// This class is now unnecessary since OperationResult has been made generic
    /// </summary>
    //public class OperationResultDecimal
    //{
    //    public OperationResultDecimal()
    //    {
    //    }

    //    public OperationResultDecimal(decimal result, string message) : this()
    //    {
    //        this.Result = result;
    //        this.Message = message;
    //    }

    //    public decimal Result { get; set; }
    //    public string Message { get; set; }
    //}
}
