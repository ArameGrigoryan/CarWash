using System.ComponentModel.DataAnnotations;

namespace CarWash.Core.Models;

public class WashStation
{
    [Key]
    public int Id { get; set; }

    public bool IsActive { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}