using MilleniumCardApp.Models;

namespace MilleniumCardApp.API;

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);