using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Extension;


public class UI_ShopPopup : UI_Popup
{
    enum Buttons
    {
        ShopResetButton,
        ExitButton,
        DeleteCardButton
    }
    enum Texts
    {
        ResetMoneyText,
    }
    enum Transforms
    {
        CardTransForm1,
        CardTransForm2,
        CardTransForm3,
        CardTransForm4,
        CardTransForm5,
        CardTransForm6,
        ItemTransForm1,
        ItemTransForm2,
        ItemTransForm3,
    }

    public override bool Init()
    {
        
        Bind<Transform>(typeof(Transforms));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetText((int)Texts.ResetMoneyText).text = Managers.Game.ShopResetMoney.ToString() + " °ñµå";
        GetButton((int)Buttons.ShopResetButton).gameObject.BindEvent(ShopResetButton);
        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(ExitShop);
        GetButton((int)Buttons.DeleteCardButton).gameObject.BindEvent(DeleteCardOn);
        InitShop();
        return true;
    }
    public void DeleteCardOn()
    {
        Managers.UI.ShowPopupUI<UI_CardDeletePopup>();
    }
    public void ShopResetButton() {
        if (Managers.Game.Money < Managers.Game.ShopResetMoney) return;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/»óÁ¡¸®·Ñ", Managers.Game.EffectSound);

        Managers.Game.Money -= Managers.Game.ShopResetMoney;
        Managers.Game.ShopResetMoney += 5;
        GetText((int)Texts.ResetMoneyText).text = Managers.Game.ShopResetMoney.ToString() + " °ñµå";
        ShopReset();
    }
    public void ClearShop() {
        Managers.Game.ShopCards.Clear();
        Managers.Game.ShopItems.Clear();
        Managers.Game.ShopBuyCards.Clear();
        Managers.Game.ShopBuyItems.Clear();
        for (int i = shoplist.Count - 1; i >= 0; i--) {
            Destroy(shoplist[i]);
        }
        shoplist.Clear();
    }
    public void ExitShop() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        GetComponent<Animator>().SetTrigger("Off");
    }
    public void OnComplete() {
        Managers.UI.FindPopup<UI_MapPopup>().SideBarOn();
        Managers.UI.ClosePopupUI(this);
    }
    public List<GameObject> shoplist = new();
    public void ShopReset() {
        ClearShop();
        CardData Randcard;
     
        for (int i = 0; i < 6; i++)
        {
            do
            {
                Randcard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
            } while (Randcard.ID >= 119 || Randcard.ID == 1 || Randcard.ID == 2 || Randcard.ID == 49 || Randcard.ID == 50 || Managers.Game.ShopCards.Contains(Randcard.ID));
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_ShopCard", transform);
            card.transform.position = Get<Transform>(i).position;
            shoplist.Add(card);
            var cardData = card.GetComponent<UI_NonBattleCard>();
            cardData.SetInfo(Randcard.ID);
            Managers.Game.ShopCards.Add(Randcard.ID);
        }
        ItemData RandItem;
        do
        {
            RandItem = Managers.Data.Items.ElementAt(_rand.Next(0, Managers.Data.Items.Count)).Value;
        } while (!((RandItem.rarity == Define.Rarity.Normal || RandItem.rarity == Define.Rarity.Rare) && RandItem.ID >= 10));
        var item = Managers.Resource.Instantiate("UI/SubItem/UI_ShopItem",transform);
        item.transform.position = Get<Transform>((int)Transforms.ItemTransForm1).position;
        shoplist.Add(item);
        var ui_item = item.GetComponent<UI_Item>();
        ui_item.SetInfo(RandItem.ID, 4);
        Managers.Game.ShopItems.Add(RandItem.ID);

        do
        {
            RandItem = Managers.Data.Items.ElementAt(_rand.Next(0, Managers.Data.Items.Count)).Value;
        } while (!(RandItem.rarity == Define.Rarity.Unique && RandItem.ID >= 10));
        item = Managers.Resource.Instantiate("UI/SubItem/UI_ShopItem", transform);
        item.transform.position = Get<Transform>((int)Transforms.ItemTransForm2).position;
        shoplist.Add(item);
        ui_item = item.GetComponent<UI_Item>();
        ui_item.SetInfo(RandItem.ID, 4);
        Managers.Game.ShopItems.Add(RandItem.ID);

        do
        {
            RandItem = Managers.Data.Items.ElementAt(_rand.Next(0, Managers.Data.Items.Count)).Value;
        } while (!(RandItem.rarity == Define.Rarity.Legend && RandItem.ID >= 10));
        item = Managers.Resource.Instantiate("UI/SubItem/UI_ShopItem", transform);
        item.transform.position = Get<Transform>((int)Transforms.ItemTransForm3).position;
        shoplist.Add(item);
        ui_item = item.GetComponent<UI_Item>();
        ui_item.SetInfo(RandItem.ID, 4);
        Managers.Game.ShopItems.Add(RandItem.ID);

        Managers.Game.SaveGame();

    }
    public void InitShop() {
        if (Managers.Game.ShopCards.Count == 0)
        {
            ShopReset();
        }
        else {
            for (int i = 0; i < Managers.Game.ShopCards.Count; i++) {
                if (Managers.Game.ShopBuyCards.Contains(Managers.Game.ShopCards[i])) { continue; }
                var card = Managers.Resource.Instantiate("UI/SubItem/UI_ShopCard", transform);
                card.transform.position = Get<Transform>(i).position;
                shoplist.Add(card);
                var cardData = card.GetComponent<UI_NonBattleCard>();
                cardData.SetInfo(Managers.Game.ShopCards[i]);
            }
            for (int i = 6; i < Managers.Game.ShopItems.Count + 6; i++) {
                if (Managers.Game.ShopBuyItems.Contains(Managers.Game.ShopItems[i-6])) { continue; }
                var item = Managers.Resource.Instantiate("UI/SubItem/UI_ShopItem", transform);
                item.transform.position = Get<Transform>(i).position;
                shoplist.Add(item);
                var ui_item = item.GetComponent<UI_Item>();
                ui_item.SetInfo(Managers.Game.ShopItems[i - 6], 4);
            }
        }
    }
}
