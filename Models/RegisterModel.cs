using System.ComponentModel.DataAnnotations;

namespace dot_Net_web_api.Models
{
    public class Registermodel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;     
        public string UserName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;


    }
}
