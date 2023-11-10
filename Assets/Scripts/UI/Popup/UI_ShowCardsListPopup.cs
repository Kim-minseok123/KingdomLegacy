using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowCardsListPopup : UI_Popup
{
    List<int> cards = new();
    enum Buttons { 
        EndButton,
    }
    enum Transforms { 
        CardsList,
    }
    public override bool Init()
    {
        if(!base.Init()) return false;

        GetComponent<Canvas>().sortingOrder = 998;

        BindButton(typeof(Buttons));
        Bind<Transform>(typeof(Transforms));
        GetButton((int)Buttons.EndButton).gameObject.BindEvent(EndButton);

        ShowCardsList();

        return true;
    }
    public void EndButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        GetComponent<Animator>().SetTrigger("Off");
    }
    public void OnComplete() {
        Managers.UI.ClosePopupUI(this);
    }
    public void SetInfo(List<CardData> cardDatas = null) {
        if (cardDatas != null)
        {
            for (int i = 0; i < cardDatas.Count; i++)
            {
                cards.Add(cardDatas[i].ID);
            }
        }
        else {
            for (int i = 0; i < Managers.Game.Cards.Count; i++) {
                cards.Add(Managers.Game.Cards[i]);
            }
        }
    }
    public void ShowCardsList() {
        for (int i = 0; i < cards.Count; i++)
        {
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_NonBattleCard", Get<Transform>((int)Transforms.CardsList));
            card.GetComponent<UI_NonBattleCard>().SetInfo(cards[i]);
        }
    }
}
