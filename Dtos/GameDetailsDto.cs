namespace Dtos.GameDto;

// DTO is a contract between a client and server since it represents how data will be transferred and used
public record GameDetailsDto
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);