namespace DemoPortal.Backend.Documents.Api.Contract;

public class DocumentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
}