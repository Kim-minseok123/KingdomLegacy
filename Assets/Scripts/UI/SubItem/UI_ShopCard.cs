using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShopCard : UI_NonBattleCard
{
    public override bool Init() { 
        base.Init();
        Money.text = _cardData.price.ToString();
        return true; 
    }
    public TextMeshProUGUI Money;
    public override void ClickCard()
    {
        if (Managers.Game.Money < _cardData.price) return;

        CardData card = GameEvents.OnGetCard(_cardData);
        card ??= _cardData;
        Managers.Game.Cards.Add(card.ID);
        Managers.Game.Money -= card.price;
        Managers.Game.ShopBuyCards.Add(card.ID);
        Managers.Game.SaveGame();
        Destroy(gameObject);
    }
}
