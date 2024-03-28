using System.ComponentModel.DataAnnotations;

namespace STSPro.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
