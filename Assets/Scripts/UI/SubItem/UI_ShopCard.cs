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

        GameEvents.OnGetCard();
        CardData card = null;
        if (_cardData.type == Define.CardType.Attack)
            card = GameEvents.OnGetAttackCard(_cardData);
        if (_cardData.type == Define.CardType.Skill)
            card = GameEvents.OnGetSkillCard(_cardData);
        if (_cardData.type == Define.CardType.Friend)
            card = GameEvents.OnGetFriendCard(_cardData);
        if (card == null)
        {
            Managers.Game.ShopBuyCards.Add(_cardData.ID);

            Managers.Game.Cards.Add(_cardData.ID);
        }
        else if (card != null)
        {
            Managers.Game.ShopBuyCards.Add(card.ID);

            Managers.Game.Cards.Add(card.ID);
        }
        Managers.Game.Money -= price;
        
        Managers.Game.SaveGame();
        Destroy(gameObject);
    }
    public void Update()
    {

        if (Managers.Game.isDiscount) {
            Money.text = (_cardData.price / 2).ToString();
            if (Managers.Game.Money < (_cardData.price / 2)) {
                Money.text = "<color=red>" + Money.text + "</color>";
            }
        }
        else { 
            Money.text = (_cardData.price).ToString();
            if (Managers.Game.Money < _cardData.price)
            {
                Money.text = "<color=red>" + Money.text + "</color>";
            }
        }
        
    }
}
