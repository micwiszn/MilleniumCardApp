using MilleniumCardApp.API.Interfaces;
using MilleniumCardApp.API.Repositories;
using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Services;

public enum CardDetailsResponseStatus
{
    Success,
    CardNotFound,
    //to increase with more cases
}


public class CardService
{
    private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();
    private readonly ICardActionsRepository _cardActionsRepository;
    
    public CardService(ICardActionsRepository cardActionsRepository)
    {
        _cardActionsRepository = cardActionsRepository;
    }
    
    public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
    {
// At this point, we would typically make an HTTP call to an external service
// to fetch the data. For this example we use generated sample data.
        await Task.Delay(1000);
        if (!_userCards.TryGetValue(userId, out var cards)
            || !cards.TryGetValue(cardNumber, out var cardDetails))
        {
            return null;
        }
        return cardDetails;
    }
    
    public async Task<(CardDetailsResponseStatus status, CardDetails? details, IEnumerable<CardAction> actions)> GetCardDetailsWithActions(string userId, string cardNumber)
    {
        var cardDetails = await GetCardDetails(userId, cardNumber);
        
        if (cardDetails is null)
        {
            return new (CardDetailsResponseStatus.CardNotFound, null, Array.Empty<CardAction>());
        }
        var actions = await _cardActionsRepository.GetActions();
        actions = MatchAllowedActions(cardDetails, actions);
        
        return new (CardDetailsResponseStatus.Success, cardDetails, actions);
    }

    private static IEnumerable<CardAction> MatchAllowedActions(CardDetails cardDetails, IEnumerable<CardAction> actions)
    {
        var hasPin = cardDetails.IsPinSet;

        return actions.Where(a =>
            a.AcceptedCardTypes.Contains(cardDetails.CardType)
            && a.AcceptedCardStatuses.Any(rule =>
                rule.CardStatus == cardDetails.CardStatus
                && PinMatches(hasPin, rule.CardOnPinBehaviour)
            ));
    }
    
    private static bool PinMatches(bool hasPin, CardOnPinBehaviour? required) =>
        required switch
        {
            null  => true,
            CardOnPinBehaviour.Default => true,
            CardOnPinBehaviour.FalseOnPin => !hasPin,
            CardOnPinBehaviour.TrueOnPin => hasPin,
            _ => false
        };
    
    private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
    {
        var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
        for (var i = 1; i <= 3; i++)
        {
            var cards = new Dictionary<string, CardDetails>();
            var cardIndex = 1;
            foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                {
                    var cardNumber = $"Card{i}{cardIndex}";
                    cards.Add(cardNumber,
                        new CardDetails(
                            CardNumber: cardNumber,
                            CardType: cardType,
                            CardStatus: cardStatus,
                            IsPinSet: cardIndex % 2 == 0));
                    cardIndex++;
                }
            }
            var userId = $"User{i}";
            userCards.Add(userId, cards);
        }
        return userCards;
    }
}