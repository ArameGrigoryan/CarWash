using CarWash.Core.Models.Enums;

namespace CarWash.Core.DTOs;
public class BookingDto
{
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public int ServiceId { get; set; }
    public int WashStationId { get; set; }
    public DateTime StartTime { get; set; }
    public BookingStatus Status { get; set; }
    
    //public DateTime EndTime { get; set; }
}