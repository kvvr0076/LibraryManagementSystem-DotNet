namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public int Quantity { get; set; }

        public List<BorrowRecord> BorrowRecords { get; set; } = new();
    }

}
