using System.ComponentModel.DataAnnotations;
using DemoPortal.Backend.Documents.DataAccess.Sql.Abstractions;

namespace DemoPortal.Backend.Documents.DataAccess.Sql.Entities;

public class Document : BaseEntity
{
    public Guid UserId { get; set; }
    
    [StringLength(256)]
    public string Title { get; set; }
    
    [StringLength(512)]
    public string Text { get; set; }
}