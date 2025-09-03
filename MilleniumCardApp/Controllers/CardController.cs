using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MilleniumCardApp.API.Requests;
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
    public async Task<ActionResult<CardDetails>> GetCardInfo(CardInfoRequest request)
    {
        var cards = await _cardService.GetCardDetails(request.UserId, request.CardId);
        Console.WriteLine(cards);
        return Ok(cards);
    }
}