namespace DemoPortal.Backend.Documents.Abstractions.Models;

public class DocumentCreateModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}