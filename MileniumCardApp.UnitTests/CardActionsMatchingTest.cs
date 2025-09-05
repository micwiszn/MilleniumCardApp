using FluentAssertions;
using MilleniumCardApp.API.Interfaces;
using MilleniumCardApp.API.Services;
using MilleniumCardApp.Models;
using Moq;

namespace MileniumCardAppTests;

public class CardActionsMatchingTest
{
    private const string USER_ID = "User1";
    
    [Fact]
    public async Task TestCardActionsMatchingThroughService()
    {
        var userCards = CreateSampleUserCards();

        var creditBlocked = userCards[USER_ID]
            .Values
            .Where(c => c.CardType == CardType.Credit
                                 && c.CardStatus == CardStatus.Blocked
                                 )
            .ToList();
        
        var prepaidClosed = userCards[USER_ID]
            .Values
            .Where(c => c.CardType == CardType.Prepaid
                         && c.CardStatus == CardStatus.Closed)
            .ToList();
        
        var debitActive = userCards[USER_ID]
            .Values
            .Where(c => c.CardType == CardType.Debit
                        && c.CardStatus == CardStatus.Active)
            .ToList();
        
        var cardActions = new[]
        {
            new CardAction("Credit-Blocked", [CardType.Credit], [(CardStatus.Blocked, null)]),
            new CardAction("Prepaid-Closed", [CardType.Prepaid], [(CardStatus.Closed, null)]),
            new CardAction("Debit-Active-NoPin", [CardType.Debit], [(CardStatus.Active, CardOnPinBehaviour.FalseOnPin)]),
            new CardAction("Debit-Active-Default", [CardType.Debit], [(CardStatus.Active, CardOnPinBehaviour.Default)])
        };

        var repo = new Mock<ICardActionsRepository>();
        repo.Setup(r => r.GetActions()).ReturnsAsync(cardActions);

        var svc = new CardService(repo.Object);

        foreach (var card in creditBlocked)
        {
            var (status, details, actions) = await svc.GetCardDetailsWithActions(
                userId: USER_ID,
                cardNumber: card.CardNumber
            );

            status.Should().Be(CardDetailsResponseStatus.Success);
            actions.Select(a => a.ActionName).Should().BeEquivalentTo("Credit-Blocked");  
        }
        
        foreach (var card in prepaidClosed)
        {
            var (status, details, actions) = await svc.GetCardDetailsWithActions(
                userId: USER_ID,
                cardNumber: card.CardNumber
            );

            status.Should().Be(CardDetailsResponseStatus.Success);
            actions.Select(a => a.ActionName).Should().BeEquivalentTo("Prepaid-Closed");  
        }
        
        
        foreach (var card in debitActive)
        {
            var (status, details, actions) = await svc.GetCardDetailsWithActions(
                userId: USER_ID,
                cardNumber: card.CardNumber
            );

            status.Should().Be(CardDetailsResponseStatus.Success);
            details.Should().NotBeNull();

            var actionNames = actions.Select(a => a.ActionName).ToArray();

            // must always include the default
            actionNames.Should().Contain("Debit-Active-Default");

            // pin-conditional expectation
            if (!details!.IsPinSet)
                actionNames.Should().Contain("Debit-Active-NoPin");
            else
                actionNames.Should().NotContain("Debit-Active-NoPin");
        }
        

    }
    
    
    //rip off from the provided service - just limit to User1
    private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
    {
        var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();

            var cards = new Dictionary<string, CardDetails>();
            var cardIndex = 1;
            foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                {
                    var cardNumber = $"Card1{cardIndex}";
                    cards.Add(cardNumber,
                        new CardDetails(
                            CardNumber: cardNumber,
                            CardType: cardType,
                            CardStatus: cardStatus,
                            IsPinSet: cardIndex % 2 == 0));
                    cardIndex++;
                }
            }
            var userId = USER_ID;
            userCards.Add(userId, cards);
        return userCards;
    }
}