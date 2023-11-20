namespace DemoPortal.Backend.Documents.DataAccess.Sql.Abstractions
{
    public interface IHaveEditDates
    {
        DateTime CreatedOnUtc { get; set; }
        DateTime ModifiedOnUtc { get; set; }
    }
}
