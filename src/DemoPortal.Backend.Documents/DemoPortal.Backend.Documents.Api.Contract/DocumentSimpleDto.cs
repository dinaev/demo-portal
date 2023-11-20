namespace DemoPortal.Backend.Documents.Api.Contract;

public class DocumentSimpleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
}