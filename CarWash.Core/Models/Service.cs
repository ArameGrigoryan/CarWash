using System.ComponentModel.DataAnnotations;

namespace CarWash.Core.Models;

public class Service
{
    [Key] public int Id { get; set; }
    [Required] public TimeSpan Duration { get; set; }
    private string _name = string.Empty;
    [Required] [Range(0, int.MaxValue)] 
    public int Price { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;

            Price = value.ToLower() switch
            {
                "wash" => 3000,
                "wax" => 5000,
                "polish" => 7000,
                "interior cleaning" => 7000,
                _ => 0
            };
        }
    }
}    