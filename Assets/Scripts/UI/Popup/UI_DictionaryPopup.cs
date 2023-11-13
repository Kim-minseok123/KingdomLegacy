using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DictionaryPopup : UI_Popup
{
    enum Buttons {
        ExitButton,
    }
    enum Transforms {
        CardPosition,
        CardButtonList,
    }
    GameObject curCard;
    public override bool Init()
    {
        if (!base.Init())
            return false;
        BindButton(typeof(Buttons));
        Bind<Transform>(typeof(Transforms));

        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(ExitDictionary);

        CreateDictionaryButton();

        return true;
    }
    public void ExitDictionary()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        GetComponent<Animator>().SetTrigger("Off");
    }
    public void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
    public void CreateDictionaryButton() {
        int Count = Managers.Data.Cards.Count;
        for (int i = 0; i < Count; i++) {
            var CardBtn = Managers.Resource.Instantiate("UI/SubItem/UI_DictionaryCardButton", Get<Transform>((int)Transforms.CardButtonList));
            CardBtn.GetComponent<UI_DictionaryCardButton>().SetInfo(i+1);
        }
    }
    public void ShowCard(int id) {
        if (curCard != null) { 
            Destroy(curCard);
            curCard = null;
        }
        curCard = Managers.Resource.Instantiate("UI/SubItem/UI_NonBattleCard", Get<Transform>((int)Transforms.CardPosition));
        curCard.transform.localScale = new Vector3(2f, 2f);
        curCard.GetComponent<UI_NonBattleCard>().SetInfo(id);
    }
}
