using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.AuthorDTO
{
    public class EditAuthorDTO
    {
        [Required]
        public int id { get; set; }
        public string name { get; set; }
        public string bio { get; set; }
        public int numberOfBooks { get; set; }
        public int age { get; set; }

    }
}
