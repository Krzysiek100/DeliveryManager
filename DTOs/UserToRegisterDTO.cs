using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public enum RoleName
    {
        Admin, 
        DeliveryMan,
        Member
    }
    public class UserToRegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength=5, ErrorMessage="Password must have at least 5 characters")]
        public string Password { get; set; }
 
    }
}