using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Responses;

public record CardActionGenerateResponse(IEnumerable<CardActionResponse> Actions);
