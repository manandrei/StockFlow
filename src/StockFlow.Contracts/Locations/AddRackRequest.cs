using System.ComponentModel.DataAnnotations;

namespace StockFlow.Contracts.Locations;

public record AddRackRequest
{
    [Required, MaxLength(10, ErrorMessage = "Rack Name cannot be longer than 10 characters"), MinLength(1, ErrorMessage = "Rack Name cannot be shorter than 1 character")]
    public string Name { get; init; }
}