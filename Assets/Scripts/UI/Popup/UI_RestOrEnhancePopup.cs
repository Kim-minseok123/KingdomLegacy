using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UI_RestOrEnhancePopup : UI_Popup
{
    enum Images { 
        Background,
    }
    enum Buttons { 
        RestButton,
        EnhanceButton,
        ExitButton,
    }
    enum Texts { 
        ContentsText,
    }
    public bool isEnhacne = false;
    public override bool Init()
    {
        if (!base.Init()) {
            return false;
        }
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        GetImage((int)Images.Background).sprite = Managers.Resource.Load<Sprite>($"Sprites/BattleGround/BattleGround{Managers.Game.Stage}");
        GetText((int)Texts.ContentsText).DOFade(0, 0.1f);
        GetButton((int)Buttons.RestButton).gameObject.BindEvent(RestButton);
        GetButton((int)Buttons.RestButton).gameObject.BindEvent((go) => { PointEnterRest(); },Define.UIEvent.PointerEnter);
        GetButton((int)Buttons.RestButton).gameObject.BindEvent(PointLeaveRest, Define.UIEvent.PointerExit);
        GetButton((int)Buttons.EnhanceButton).gameObject.BindEvent(EnhanceButton);
        GetButton((int)Buttons.EnhanceButton).gameObject.BindEvent((go) => { PointEnterEnhance(); }, Define.UIEvent.PointerEnter);
        GetButton((int)Buttons.EnhanceButton).gameObject.BindEvent(PointLeaveEnhance, Define.UIEvent.PointerExit);

        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(ExitButton);
        if (Managers.Game.isRest) {
            GetButton((int)Buttons.RestButton).interactable = false;
        }
        if (Managers.Game.isEnhance)
        {
            GetButton((int)Buttons.EnhanceButton).interactable = false;
        }
        return true;
    }
    public void ExitButton() {

        if (Managers.UI.PeekPopupUI<UI_RestOrEnhancePopup>() != this)
            return;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        GetComponent<Animator>().SetTrigger("Off");
    }
    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.FindPopup<UI_MapPopup>().SideBarOn();
        Managers.Game.SaveGame();
    }
    public void RestButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/회복", Managers.Game.EffectSound);

        if (Managers.Game.isRest) return;
        if (isEnhacne) return;

        int HealHp = (int)(Managers.Game.MaxHp * 0.3f);
        Managers.Game.CurHp += HealHp;
        if (Managers.Game.CurHp > Managers.Game.MaxHp) { 
            Managers.Game.CurHp = Managers.Game.MaxHp;
        }
        ExitButton();
    }
    public void EnhanceButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        if (Managers.Game.isEnhance) return;
        if (isEnhacne) return;

        Managers.UI.ShowPopupUI<UI_EnhancePopup>();
    }
    public void PointEnterRest() {
        GetButton((int)Buttons.RestButton).transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.2f).SetEase(Ease.OutSine);
        GetText((int)Texts.ContentsText).text = "최대 체력의 30% 만큼 체력을 회복합니다.";
        GetText((int)Texts.ContentsText).DOFade(1,0.2f);
    }
    public void PointEnterEnhance() { 
        GetButton((int)Buttons.EnhanceButton).transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.2f).SetEase(Ease.OutSine);
        GetText((int)Texts.ContentsText).text = "덱에서 강화되지 않은 카드 1 장을 강화합니다.";
        GetText((int)Texts.ContentsText).DOFade(1, 0.2f);
    }
    public void PointLeaveRest()
    {
        GetButton((int)Buttons.RestButton).transform.DOScale(new Vector3(1f, 1f, 1), 0.2f).SetEase(Ease.OutSine);
        GetText((int)Texts.ContentsText).DOFade(0, 0.2f);
    }
    public void PointLeaveEnhance()
    {
        GetButton((int)Buttons.EnhanceButton).transform.DOScale(new Vector3(1f, 1f, 1), 0.2f).SetEase(Ease.OutSine);
        GetText((int)Texts.ContentsText).DOFade(0, 0.2f);
    }
    public void Enhance()
    {
        isEnhacne = true;
        GetButton((int)Buttons.RestButton).interactable = false;
        GetButton((int)Buttons.EnhanceButton).interactable = false;
        if (Managers.UI.PeekPopupUI<UI_EnhancePopup>() == Managers.UI.FindPopup<UI_EnhancePopup>()) {
            Managers.UI.ClosePopupUI();
        }
        ExitButton();
    }

}
