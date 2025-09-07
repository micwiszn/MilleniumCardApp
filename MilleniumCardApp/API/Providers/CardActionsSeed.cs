using MilleniumCardApp.Models;

namespace MilleniumCardApp.API.Providers;

public static class CardActionsSeed
{
    public static IReadOnlyList<CardAction> All { get; } = [
    new("ACTION1",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Active, null)]
    ),
    new("ACTION2",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Inactive, null)]
    ),
    new("ACTION3",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, null),
            (CardStatus.Inactive, null),
            (CardStatus.Active, null),
            (CardStatus.Restricted, null),
            (CardStatus.Blocked, null),
            (CardStatus.Expired, null),
            (CardStatus.Closed, null)]
    ),
    new("ACTION4",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],[(CardStatus.Ordered, null),
            (CardStatus.Inactive, null),
            (CardStatus.Active, null),
            (CardStatus.Restricted, null),
            (CardStatus.Blocked, null),
            (CardStatus.Expired, null),
            (CardStatus.Closed, null)]
    ),
    new("ACTION5",
        [CardType.Credit],
        [(CardStatus.Ordered, null),
        (CardStatus.Inactive, null),
        (CardStatus.Active, null),
        (CardStatus.Restricted, null),
        (CardStatus.Blocked, null),
        (CardStatus.Expired, null),
        (CardStatus.Closed, null)]
    ),
    new("ACTION6",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, CardOnPinBehaviour.TrueOnPin),
         (CardStatus.Inactive, CardOnPinBehaviour.TrueOnPin),
         (CardStatus.Active, CardOnPinBehaviour.TrueOnPin),
         (CardStatus.Restricted, CardOnPinBehaviour.TrueOnPin)]
    ),
    new("ACTION7",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, CardOnPinBehaviour.FalseOnPin),
         (CardStatus.Inactive, CardOnPinBehaviour.FalseOnPin),
         (CardStatus.Active, CardOnPinBehaviour.FalseOnPin),
         (CardStatus.Restricted, CardOnPinBehaviour.TrueOnPin)]
    ),
    new("ACTION8",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, null),
         (CardStatus.Inactive, null),
         (CardStatus.Active, null),
         (CardStatus.Blocked, null)]
    ),
    new("ACTION9",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, null),
         (CardStatus.Inactive, null),
         (CardStatus.Active, null),
         (CardStatus.Restricted, null),
         (CardStatus.Blocked, null),
         (CardStatus.Expired, null),
         (CardStatus.Closed, null)]
    ),
    new("ACTION10",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, null),
         (CardStatus.Inactive, null),
         (CardStatus.Active, null)]
    ),
    new("ACTION11",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Inactive, null),
         (CardStatus.Active, null)]
    ),
    new("ACTION12",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, null),
            (CardStatus.Inactive, null),
            (CardStatus.Active, null)]
    ),
    new("ACTION13",
        [CardType.Prepaid, CardType.Debit, CardType.Credit],
        [(CardStatus.Ordered, null),
         (CardStatus.Inactive, null),
         (CardStatus.Active, null)]
    )
];
}