using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoPortal.Backend.Documents.DataAccess.Sql.Abstractions
{
    public abstract class BaseEntity : IHaveEditDates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime ModifiedOnUtc { get; set; }
    }
}