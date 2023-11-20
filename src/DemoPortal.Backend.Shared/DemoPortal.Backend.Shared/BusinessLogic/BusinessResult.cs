namespace DemoPortal.Backend.Shared.BusinessLogic
{
    public class BusinessResult<T>
    {
        public BusinessResult()
        {
        }

        public BusinessResult(T resultData)
        {
            ResultData = resultData;
            IsSuccessful = true;
        }

        public BusinessResult(ErrorModel error)
        {
            Error = error;
            IsSuccessful = false;
        }

        public bool IsSuccessful { get; set; }
        public T ResultData { get; set; }
        public ErrorModel Error { get; set; }

        public static implicit operator BusinessResult<T>(T value) => new BusinessResult<T>(value);
        public static implicit operator BusinessResult<T>(ErrorModel error) => new BusinessResult<T>(error);
    }

    public class BusinessResult
    {
        public static BusinessResult Successful => new BusinessResult();

        public BusinessResult()
        {
            IsSuccess = true;
        }

        public BusinessResult(ErrorModel error)
        {
            Error = error;
            IsSuccess = false;
        }

        public bool IsSuccess { get; set; }
        public ErrorModel Error { get; set; }

        public static implicit operator BusinessResult(ErrorModel error) => new BusinessResult(error);
    }
}
