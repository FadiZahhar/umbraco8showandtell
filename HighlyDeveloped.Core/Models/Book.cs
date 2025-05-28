using NPoco;
using System.ComponentModel.DataAnnotations;

namespace HighlyDeveloped.Core.Models
{
    [TableName("Books")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Book
    {
     
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        [Range(1450, 2100)]
        public int Year { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "ISBN must be in the format 123-1234567890")]
        public string ISBN { get; set; }
    }
}