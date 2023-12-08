namespace DemoPortal.Backend.Shared.Listing
{
    public class PagingResult<T>
    {
        public IList<T> Items { get; set; }
        public int Total { get; set; }

        public static PagingResult<T> Empty => new PagingResult<T> {Items = new List<T>(0)};
    }
}