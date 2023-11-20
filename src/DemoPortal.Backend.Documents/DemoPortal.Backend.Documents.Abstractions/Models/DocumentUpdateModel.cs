namespace DemoPortal.Backend.Documents.Abstractions.Models;

public class DocumentUpdateModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}