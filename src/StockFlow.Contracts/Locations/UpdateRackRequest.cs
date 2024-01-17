using System.ComponentModel.DataAnnotations;

namespace StockFlow.Contracts.Locations;

public record UpdateRackRequest
{
    [Required(ErrorMessage = "Id is required")]
    public long Id { get; init; }

    [Required, MaxLength(10, ErrorMessage = "Rack Name cannot be longer than 10 characters"), MinLength(1, ErrorMessage = "Rack Name cannot be shorter than 1 character")]
    public required string Name { get; init; }
}