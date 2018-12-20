namespace BlazorSorting
{
    public class Parameters
    {
        public Parameters()
        {
        }

        public Parameters(string defaultSortBy)
        {
            SortBy = defaultSortBy;
            CurrentPage = 1;
        }

        public Direction Direction { get; set; }

        public string SortBy { get; set; }

        public string Filter { get; set; }

        public int CurrentPage { get; set; }

        public void SetSortBy(string value)
        {
            if (SortBy == value)
                Direction = Direction == Direction.Ascending
                    ? Direction.Descending
                    : Direction.Ascending;
            else
            {
                Direction = Direction.Ascending;
                SortBy = value;
                CurrentPage = 1;
            }
        }
        public void SetPage(int value)
        {
            CurrentPage = value;
        }

    }
}


