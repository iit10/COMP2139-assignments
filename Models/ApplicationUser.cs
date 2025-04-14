using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SmartInventory3.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string ContactInfo { get; set; }
    
    
    
}