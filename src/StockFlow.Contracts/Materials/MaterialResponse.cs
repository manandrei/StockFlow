using System.ComponentModel.DataAnnotations;

namespace StockFlow.Contracts.Materials;

public record MaterialResponse
{
    public MaterialResponse(long Id, string PartNumber, string SizeType, string? Description)
    {
        this.Id = Id;
        this.PartNumber = PartNumber;
        this.SizeType = SizeType;
        this.Description = Description;
    }

    [Required]
    public long Id { get; init; }
    [Required]
    [MaxLength(7, ErrorMessage = "Part number cannot be longer than 7 characters"), MinLength(3, ErrorMessage = "Part number cannot be shorter than 3 characters")]
    public string PartNumber { get; init; }
    [Required]
    //Todo: convert to enum and make it required and convertible to the domain enum type (StockFlow.Domain.Materials.SizeType)
    public string SizeType { get; init; }
    public string? Description { get; init; }
    
};