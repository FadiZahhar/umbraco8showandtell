using NPoco;

namespace HighlyDeveloped.Core.Models
{
    [TableName("Books")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
    }
}