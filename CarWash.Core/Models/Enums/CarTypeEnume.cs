using System.ComponentModel.DataAnnotations;

namespace CarWash.Core.Models.Enums;

public enum CarType
{
    [Display(Name = "Sedan")]
    Sedan = 1,
    [Display(Name = "Jeep")]
    Jeep = 2,
    [Display(Name = "Hashback")]
    Hashback = 3,
    [Display(Name = "Truck")]
    Truck = 4,
    [Display(Name = "Minivan")]
    Minivan = 5
};