namespace EmpleosSur.Domain.Helpers
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static OperationResult SuccessResult()
        {
            return new OperationResult { Success = true };
        }

        public static OperationResult Failure(string errorMessage)
        {
            return new OperationResult { Success = false, ErrorMessage = errorMessage };
        }
    }
}
