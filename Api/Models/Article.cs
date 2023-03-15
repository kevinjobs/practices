using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int CreateAt { get; set; }
    }
}