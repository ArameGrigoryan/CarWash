using System.ComponentModel.DataAnnotations;

namespace CarWash.Core.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string .Empty;
    
    [Required]
    [MaxLength(25)]
    public string Phone  { get; set; } = string.Empty;
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Car>  Cars { get; set; } =  new List<Car>();
}