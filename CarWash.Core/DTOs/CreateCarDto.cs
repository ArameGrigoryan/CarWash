using System.ComponentModel;
using System.Text.Json.Serialization;
using CarWash.Core.Models.Enums;

namespace CarWash.Core.DTOs;

public class CreateCarDto
{
    [DefaultValue("1---Sedan, 2---Jeep, 3---Hatchback, 4---Truck, 5---Minivan")]
    [JsonConverter(typeof(JsonNumberEnumConverter<CarType>))]
    public CarType Type { get; set; }
}