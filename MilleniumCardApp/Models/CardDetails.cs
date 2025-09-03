namespace MilleniumCardApp.Models;

public enum CardStatus
{
    Ordered,
    Inactive,
    Active,
    Restricted,
    Blocked,
    Expired,
    Closed
}

public enum CardType
{
    Prepaid,
    Debit,
    Credit
}

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);