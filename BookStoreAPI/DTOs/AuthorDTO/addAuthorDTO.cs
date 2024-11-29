using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.DTOs.CustomerDTO
{
    public class addAuthorDTO
    {
        public string fullname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "invalid pasword")]
        public string password { get; set; }
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string email { get; set; }

        [Required]
        public string phonenumber { get; set; }

        public string name { get; set; }
        public string bio { get; set; }
        public int numberOfBooks { get; set; }
        public int age { get; set; }

    }
}
