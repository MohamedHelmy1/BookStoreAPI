using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTO
{
    public class EditAuthorDTO
    {
        [Required]
        public string id { get; set; }
        public string fullname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
         public string email { get; set; }

        public string address { get; set; }
        [Required]
        public string phonenumber { get; set; }
        public string bio { get; set; }
        public int numberOfBooks { get; set; }
        public int age { get; set; }
    }
}
