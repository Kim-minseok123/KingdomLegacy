using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClearCard : UI_NonBattleCard
{
    public override void ClickCard()
    {
        CardData card =  GameEvents.OnGetCard(_cardData);
        card ??= _cardData;
        Managers.Game.Cards.Add(card.ID);
        Managers.Sound.Play(Define.Sound.Effect, "Effect/È¹µæ", Managers.Game.EffectSound);

        Managers.UI.FindPopup<UI_ChooseClearCardPopup>().EndSelect();
    }
}
