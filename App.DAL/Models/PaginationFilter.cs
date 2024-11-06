namespace App.DAL.Models
{
    public class PaginationFilter
    {
        public int Start { get; set; } = 0;
        public int Length { get; set; } = 10;
        public string? SearchValue { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
    }
}
