namespace DemoPortal.Backend.Documents.Abstractions.Models;

public class DocumentGetSimpleModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
}