using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Responses;

public record CardActionResponse(
    string ActionName,
    IEnumerable<CardType> AcceptedCardTypes,
    IEnumerable<CardActionWithPinBehaviour> AcceptedCardStatuses)
{
    public CardActionResponse(CardAction model) : this (
        model.ActionName,
        model.AcceptedCardTypes,
        model.AcceptedCardStatuses.Select(t => new CardActionWithPinBehaviour(t.CardStatus, t.CardOnPinBehaviour)))
    {
    }
}

public record CardActionWithPinBehaviour(
    CardStatus CardStatus,
    CardOnPinBehaviour? CardOnPinBehaviour);