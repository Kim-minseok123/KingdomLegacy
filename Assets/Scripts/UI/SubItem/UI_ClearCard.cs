using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClearCard : UI_NonBattleCard
{
    public override void ClickCard()
    {
        GameEvents.OnGetCard();
        CardData card = _cardData;
        if (_cardData.type == Define.CardType.Attack)
            card = GameEvents.OnGetAttackCard(_cardData);
        if (_cardData.type == Define.CardType.Skill)
            card = GameEvents.OnGetSkillCard(_cardData);
        if (_cardData.type == Define.CardType.Friend)
            card = GameEvents.OnGetFriendCard(_cardData);
        Managers.Game.Cards.Add(card.ID);
        Managers.Sound.Play(Define.Sound.Effect, "Effect/È¹µæ", Managers.Game.EffectSound);

        Managers.UI.FindPopup<UI_ChooseClearCardPopup>().EndSelect();
    }
}
