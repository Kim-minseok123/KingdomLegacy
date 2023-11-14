using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShopCard : UI_NonBattleCard
{
    public override bool Init() { 
        base.Init();
        return true; 
    }
    public TextMeshProUGUI Money;
    public override void ClickCard()
    {
        int price = _cardData.price;
        if (Managers.Game.isDiscount)
            price /= 2;

        if (Managers.Game.Money < price) return;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/±¸¸Å", Managers.Game.EffectSound);

        CardData card = GameEvents.OnGetCard(_cardData);
        card ??= _cardData;
        Managers.Game.Cards.Add(card.ID);
        Managers.Game.Money -= price;
        Managers.Game.ShopBuyCards.Add(card.ID);
        Managers.Game.SaveGame();
        Destroy(gameObject);
    }
    public void Update()
    {
        if (Managers.Game.isDiscount)
            Money.text = (_cardData.price / 2).ToString();
    }
}
