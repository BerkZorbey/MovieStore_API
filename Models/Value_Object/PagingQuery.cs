namespace Movie_API.Models.Value_Object
{
    public class PagingQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; } = "Name";
        public SortDirection SortingDirection { get; set; }



        public enum SortDirection 
        { 
            ASC=1,
            DESC=2
        }
    }
}
