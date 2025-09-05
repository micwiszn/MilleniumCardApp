namespace MilleniumCardApp.Models;

public record CardAction(string ActionName, IEnumerable<CardType> AcceptedCardTypes, IEnumerable<(CardStatus CardStatus, CardOnPinBehaviour? CardOnPinBehaviour)> AcceptedCardStatuses);