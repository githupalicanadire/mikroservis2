namespace Identity.API.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = default!;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;

    public string FullName => $"{FirstName} {LastName}";
}
