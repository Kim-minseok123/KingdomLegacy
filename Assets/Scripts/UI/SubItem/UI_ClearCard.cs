using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClearCard : UI_NonBattleCard
{
    public override void ClickCard()
    {
        CardData card =  GameEvents.OnGetCard(_cardData);
        if (card == null) card = _cardData;
        Managers.Game.Cards.Add(Managers.Data.Cards[card.ID]);
        Managers.UI.ClosePopupUI();
    }
}
