namespace DemoPortal.Backend.Documents.Api.Contract;

public class DocumentCreateRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}