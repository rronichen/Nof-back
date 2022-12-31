
using System.ComponentModel.DataAnnotations;


namespace Nof.Model
{
    public class User
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        [MinLength(2)]
        public string Username { get; set; }

        [MinLength(2)]
        public string Password { get; set; }


    }
}
