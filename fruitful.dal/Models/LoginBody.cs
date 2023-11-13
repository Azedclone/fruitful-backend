using System.ComponentModel.DataAnnotations;

namespace fruitful.dal.Models;

public class LoginBody
{
    [Required]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
    public string Username { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Password must be strong and at least 6 characters long.", MinimumLength = 6)]
    public string Password { get; set; }
}