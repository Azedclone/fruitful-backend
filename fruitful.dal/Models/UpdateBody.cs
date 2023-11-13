using System.ComponentModel.DataAnnotations;

namespace fruitful.dal.Models;

public class UpdateBody
{

    [Required]
    [StringLength(100, ErrorMessage = "Password must be strong and at least 6 characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; }

    public string? Address { get; set; }
    public DateTime? Dob { get; set; }
}