namespace DemoPortal.Backend.Shared.BusinessLogic
{
    public class ErrorModel
    {
        public ErrorModel(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; }
        public string Message { get; }
    }
}
