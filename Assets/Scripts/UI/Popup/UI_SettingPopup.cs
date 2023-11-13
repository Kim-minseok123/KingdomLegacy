using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingPopup : UI_Popup
{
    enum Buttons { 
        GameEndButton,
        ExitButton,
    }
    enum Sliders { 
        BgmSlider,
        EffectSlider,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        GetComponent<Canvas>().sortingOrder = 999;
        BindButton(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));
        Get<Slider>((int)Sliders.BgmSlider).value = Managers.Game.BgmSound;
        Get<Slider>((int)Sliders.EffectSlider).value = Managers.Game.EffectSound;
        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(ExitSetting);
        GetButton((int)Buttons.GameEndButton).gameObject.BindEvent(ClearGameButton);
        return true;
    }
    public void ExitSetting()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);
        Time.timeScale = 1;

        GetComponent<Animator>().SetTrigger("Off");
    }
    public void OnCanvas() {
        Time.timeScale = 0f;
    }
    public void ClearGameButton() {
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
    public void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
    public void BgmValue() {
        float value = Get<Slider>((int)Sliders.BgmSlider).value;

        Managers.Sound.SetVolume(Define.Sound.Bgm, value);
        Managers.Game.BgmSound = value;
    }
    public void EffectValue() {
        float value = Get<Slider>((int)Sliders.EffectSlider).value;

        Managers.Game.EffectSound = value;
    }
}
