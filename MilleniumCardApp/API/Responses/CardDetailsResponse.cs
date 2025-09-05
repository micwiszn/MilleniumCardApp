using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Responses;

public record CardDetailsResponse(
    string CardNumber,
    CardType CardType,
    CardStatus CardStatus,
    bool IsPinSet,
    IEnumerable<string> AllowedActions)
{

    public CardDetailsResponse(CardDetails model, IEnumerable<string>? allowedActions = null)
        : this(
            model.CardNumber,
            model.CardType,
            model.CardStatus,
            model.IsPinSet,
            allowedActions ?? Array.Empty<string>())
    {
    }
}