using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ShopItem : UI_Item
{
    public TextMeshProUGUI Money;
    public override bool Init()
    {
        base.Init();

        return true;
    }
    public override void GetItemClick()
    {
        int price = _itemData.price;
        if (Managers.Game.isDiscount)
            price /= 2;

        if (Managers.Game.Money - price < 0)
            return;
        else
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effect/±¸¸Å", Managers.Game.EffectSound);

            Managers.Game.Money -= price;
            Managers.Game.ShopBuyItems.Add(_itemData.ID);
            _itemData.ability.Setting();
            Managers.Game.Items.Add(_itemData.ID);
            Managers.UI.FindPopup<UI_MapPopup>().AddItem(_itemData.ID);
            GameEvents.OnGetItem();
            Managers.Game.SaveGame();
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (Managers.Game.isDiscount)
            Money.text = (_itemData.price / 2).ToString();
        Money.text = (_itemData.price).ToString();
    }
}

