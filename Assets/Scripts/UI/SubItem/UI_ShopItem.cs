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
        Money.text = _itemData.price.ToString();
        return true;
    }
    public override void GetItemClick()
    {

        if (Managers.Game.Money - _itemData.price < 0)
            return;
        else
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effect/±¸¸Å", Managers.Game.EffectSound);

            Managers.Game.Money -= _itemData.price;
            Managers.Game.ShopBuyItems.Add(_itemData.ID);
            _itemData.ability.Setting();
            Managers.Game.Items.Add(_itemData.ID);
            Managers.UI.FindPopup<UI_MapPopup>().AddItem(_itemData.ID);
            GameEvents.OnGetItem();
            Managers.Game.SaveGame();
            Destroy(gameObject);
        }
    }
}

