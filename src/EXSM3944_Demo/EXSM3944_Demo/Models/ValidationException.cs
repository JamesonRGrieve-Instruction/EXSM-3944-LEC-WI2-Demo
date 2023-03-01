namespace EXSM3944_Demo.Models
{
    public class ValidationException : Exception
    {
        public List<Exception> InnerExceptions = new List<Exception>();
        public override string Message => $"There are ${(InnerExceptions.Count > 0?InnerExceptions.Count:"no")} exceptions. Please view the inner exceptions for more information.";
        public bool IsError => InnerExceptions.Count > 0;
    }
}
