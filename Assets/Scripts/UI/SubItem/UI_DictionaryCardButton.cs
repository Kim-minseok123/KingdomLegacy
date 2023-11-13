using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DictionaryCardButton : UI_Base
{
    CardData cardData;
    enum Texts { 
        DesText,
    }
    public override bool Init()
    {
        if(!base.Init()) return false;
        BindText(typeof(Texts));

        GetText((int)Texts.DesText).text = $"No {cardData.ID}. {cardData.name}";

        gameObject.BindEvent(ClickButton);
        return true;
    }
    public void SetInfo(int id) {
        cardData = Managers.Data.Cards[id];
    }
    public void ClickButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        Managers.UI.FindPopup<UI_DictionaryPopup>().ShowCard(cardData.ID);
    }
}
