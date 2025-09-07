using Microsoft.AspNetCore.Mvc;
using MilleniumCardApp.API.Requests;
using MilleniumCardApp.API.Responses;
using MilleniumCardApp.API.Services;
using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }
    
    // since this was not precised out in guidelines there is an endpoint just returning all actions per card ...
    [HttpPost("card-actions-generate")]
    [ProducesResponseType(typeof(CardActionGenerateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CardDetails>> GenerateAllowedActions(CardInfoRequest request)
    {
        var cardDetailsWithActions = await _cardService.GetCardDetailsWithActions(request.UserId, request.CardId);
        
        if(cardDetailsWithActions.status == CardDetailsResponseStatus.CardNotFound)
            return NotFound();
        
        var response = new CardActionGenerateResponse(cardDetailsWithActions.actions.Select(e => new CardActionResponse(e)));
        return Ok(response);
    }
    
    // ... and endpoint returning card details extended with actions names taken form the very same service
    [HttpPost("card-details")]
    [ProducesResponseType(typeof(CardDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CardDetails>> GetCardInfo(CardInfoRequest request)
    {
        var cardDetailsWithActions = await _cardService.GetCardDetailsWithActions(request.UserId, request.CardId);
        
        if(cardDetailsWithActions.status == CardDetailsResponseStatus.CardNotFound)
            return NotFound();
        
        var response = new CardDetailsResponse(cardDetailsWithActions.details!, cardDetailsWithActions.actions.Select(e => e.ActionName));
        return Ok(response);
    }
}