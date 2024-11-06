namespace App.BL.Models
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
