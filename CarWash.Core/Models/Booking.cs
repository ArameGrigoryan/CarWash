using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarWash.Core.Models.Enums;

namespace CarWash.Core.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [Required]
    [ForeignKey(nameof(Car))]
    public int CarId { get; set; }
    public Car? Car { get; set; }
    
    [Required]
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
    
    [Required]
    [ForeignKey(nameof(WashStation))]
    public int WashStationId { get; set; }
    public WashStation? Station { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public DateTime EndTime { get; set; }
    
    [Required]
    public BookingStatus Status { get; set; }
    
    // [Required]
    // [Range(0, int.MaxValue)]
    // public int AllPrice { get; set; }
}