using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EnhancePopup : UI_Popup
{
    enum Images
    {
        Background,
    }
    enum Buttons
    {
        ExitButton,
    }
    enum Transforms { 
        UpgradeCardList,
    }
    public bool isClickUpgradeCard = false;
    public override bool Init()
    {
        if (!base.Init())
        {
            return false;
        }
        GetComponent<Canvas>().sortingOrder = 998;
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        Bind<Transform>(typeof(Transforms));
        GetImage((int)Images.Background).sprite = Managers.Resource.Load<Sprite>($"Sprites/BattleGround/BattleGround{Managers.Game.Stage}");

        CreateCards();

        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(ExitButton);
        return true;
    }
    public void ExitButton()
    {
        if (isClickUpgradeCard)
            return;
        if (Managers.UI.PeekPopupUI<UI_EnhancePopup>() != this)
            return;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        GetComponent<Animator>().SetTrigger("Off");
    }
    public void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
        
    }
    public void CreateCards() {
        var parent = Get<Transform>((int)Transforms.UpgradeCardList);
        for (int i = 0; i < Managers.Game.Cards.Count; i++) {
            if (Managers.Game.Cards[i] % 2 == 0) continue;
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_UpgradeCard", parent);
            card.GetComponent<UI_NonBattleCard>().SetInfo(Managers.Game.Cards[i]);
        }
    }
}
