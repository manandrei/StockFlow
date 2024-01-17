using System.ComponentModel.DataAnnotations;

namespace StockFlow.Contracts.Materials;

public record AddMaterialRequest
{
    [Required, MaxLength(7, ErrorMessage = "Part Number cannot be longer than 7 characters"), MinLength(3, ErrorMessage = "Part Number cannot be shorter than 3 characters")]
    public required string PartNumber { get; init; }

    [Required]
    //Todo: convert to enum and make it required and convertible to the domain enum type (StockFlow.Domain.Materials.SizeType)
    public required string SizeType { get; init; }

    public string? Description { get; init; }
}