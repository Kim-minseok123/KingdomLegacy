using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeletePopup : UI_Popup
{
    enum Texts {
        CurMoneyText,
        CurHaveMoneyText,
    }
    enum Gameobjects { 
        DeleteCard,
    }
    enum Buttons
    { 
        ExitButton,
    }

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Gameobjects));
        GetComponent<Canvas>().sortingOrder = 998;

        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(ExitMenu);

        ShowCardandText();
        return true;
    }

    public void ExitMenu()
    {
        GetComponent<Animator>().SetTrigger("Off");
    }
    public void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
    public List<GameObject> Cards = new();
    public void ShowCardandText() {
        GetText((int)Texts.CurHaveMoneyText).text = "ÇöÀç º¸À¯ °ñµå : " + Managers.Game.Money.ToString() + " °ñµå";
        GetText((int)Texts.CurMoneyText).text = "ºñ¿ë : " + Managers.Game.DeleteCardMoney.ToString() + " °ñµå";
        for (int i = Cards.Count - 1; i >= 0; i--) {
            Destroy(Cards[i]); 
        }
        Cards.Clear();
        var parent = GetObject((int)Gameobjects.DeleteCard).transform;
        for (int i = 0; i < Managers.Game.Cards.Count; i++) {
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_DeleteCard", parent);
            card.GetComponent<UI_NonBattleCard>().SetInfo(Managers.Game.Cards[i]);
            Cards.Add(card);
        }
    }
}
