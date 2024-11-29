using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Models
{
    public class Author : IdentityUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string bio { get; set; }
        public int numberOfBooks { get; set; }
        public int age { get; set; }

        public virtual List<Book> Books { get; set; } = new List<Book>();

    }
}
