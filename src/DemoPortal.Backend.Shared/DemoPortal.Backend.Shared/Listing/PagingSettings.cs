namespace DemoPortal.Backend.Shared.Listing
{
    public class PagingSettings
    {
        private const int MaxLimit = 300;

        private int _mLimit = MaxLimit;

        public int? Limit
        {
            get => _mLimit;
            set
            {
                var val = value ?? MaxLimit;
                _mLimit = val >= MaxLimit || val <= 0 ? MaxLimit : val;
            }
        }

        public int Offset { get; set; }
        public string SortColumn { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}
