using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarWash.Core.Models.Enums;

namespace CarWash.Core.Models;

public class Car
{
    [Key]
    public int Id { get; set; }
    [Required]
    public CarType Type { get; set; }
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}