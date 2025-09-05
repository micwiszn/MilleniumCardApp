using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MilleniumCardApp.API.Requests;
using MilleniumCardApp.API.Responses;
using MilleniumCardApp.API.Services;
using MilleniumCardApp.Models;

namespace MilleniumCardApp.Controllers;


[ApiController]
[Route("api/[controller]")]public class CardController : ControllerBase
{
    private readonly CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost]
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