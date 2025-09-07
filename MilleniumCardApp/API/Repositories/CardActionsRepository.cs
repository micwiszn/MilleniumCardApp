using Microsoft.Extensions.Caching.Memory;
using MilleniumCardApp.API.Providers;
using MilleniumCardApp.API.Interfaces;
using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Repositories;

public class CardActionsRepository : ICardActionsRepository
{
    //cache actions for further reducing DB calls
    private readonly IMemoryCache _memoryCache;
    private const string CARD_ACTIONS_KEY = "card_actions";

    public CardActionsRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public async Task<IEnumerable<CardAction>> GetActions()
    {
        if (!_memoryCache.TryGetValue(CARD_ACTIONS_KEY, out var actions))
        {
            //to replace with DB or something
            actions = CardActionsSeed.All;
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
            _memoryCache.Set(CARD_ACTIONS_KEY, actions, cacheOptions);
        }
        
        return actions as IEnumerable<CardAction>;
    }
}