namespace Dtos.GameDto;

// DTO is a contract between a client and server since it represents how data will be transferred and used
public record GameSummaryDto
(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);