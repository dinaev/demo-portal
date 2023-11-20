using DemoPortal.Backend.Documents.DataAccess.Sql.Abstractions;
using DemoPortal.Backend.Documents.DataAccess.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoPortal.Backend.Documents.DataAccess.Sql;

public class DocumentsContext : DbContext
{
    public DbSet<Document> Documents { get; set; }
    
    public DocumentsContext(DbContextOptions<DocumentsContext> options) : base(options)
    {
    }
    
    public override int SaveChanges()
    {
        UpdateDateTimeStamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDateTimeStamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateDateTimeStamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Modified || i.State == EntityState.Added)
            .Where(i => i.Entity is IHaveEditDates);

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            var entity = (IHaveEditDates) entry.Entity;
            if (entity == null)
                continue;

            entity.ModifiedOnUtc = now;
            if (entry.State == EntityState.Added)
                entity.CreatedOnUtc = now;
        }
    }
}