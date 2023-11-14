using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DeathPopup : UI_Popup
{
    enum Buttons { 
        GameEndButton,
    }
    enum Texts { 
        ResultText,
    }
    string _name;
    public override bool Init()
    {
        if (!base.Init())
            return false;
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Death", Managers.Game.EffectSound);

        int hours = (int)(Managers.Game.ClearTime / 3600);
        int minutes = (int)((Managers.Game.ClearTime % 3600) / 60);
        int seconds = (int)(Managers.Game.ClearTime % 60);
        GetText((int)Texts.ResultText).text = 
            $"당신은 <color=red>{_name}</color>에게 사망하셨습니다." +
            $"\n\n당신이 가진 카드는 총 <color=yellow>{Managers.Game.Cards.Count}</color>개이며, 아이템은 <color=#FF00FF>{Managers.Game.Items.Count}</color>개를 보유하였습니다." +
            $"\n\n당신은 ​<color=#9BBFEA>{hours}시간 {minutes}분 {seconds}초</color> 동안 전투하였습니다.";
        GetButton((int)Buttons.GameEndButton).gameObject.BindEvent(GoTitle);
        return true;
    }
    public void SetInfo(string info) {
        if (info == null) _name = "본인";
        else
            _name = info;
    }
    public void OnCanvas()
    {
        Time.timeScale = 0f;
    }
    public void GoTitle() {
        Time.timeScale = 1;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/넘기기", Managers.Game.EffectSound);

        TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/Brush/Brush"), 0,
                        () => {
                            Managers.UI.CloseAllPopupUI();
                            Managers.Game.ClearGame();
                            Managers.UI.ShowPopupUI<UI_TitlePopup>();
                            Managers.Sound.Play(Define.Sound.Effect, "Effect/넘기기", Managers.Game.EffectSound);
                        });
    }
}
