using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Interfaces;

public interface ICardActionsRepository
{
    public Task<IEnumerable<CardAction>> GetActions();
    
    // TO DO - add further methods to perform CRUD operations on Actions DB
}   