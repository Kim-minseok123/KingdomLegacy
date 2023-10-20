using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClearCard : UI_NonBattleCard
{
    public override void ClickCard()
    {
        Managers.Game.Cards.Add(Managers.Data.Cards[_cardData.ID]);
        Managers.UI.ClosePopupUI();
    }
}
