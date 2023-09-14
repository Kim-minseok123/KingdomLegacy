using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_ConfirmPopup : UI_Popup
{
    enum Texts
    {
        MessageText
    }

    enum Buttons
    {
        YesButton,
        NoButton
    }
    string _text;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.YesButton).gameObject.BindEvent(OnClickYesButton);
        GetButton((int)Buttons.NoButton).gameObject.BindEvent(OnClickNoButton);
        GetText((int)Texts.MessageText).text = _text;

        RefreshUI();
        return true;
    }

    Action _onClickYesButton;
    Action _onClickNoButton;
    public void SetInfo(Action onClickYesButton, string text, Action onClickNoButton = null)
    {
        _onClickYesButton = onClickYesButton;
        _onClickNoButton = onClickNoButton;
        _text = text;

        RefreshUI();
    }

    void RefreshUI()
    {
        if (_init == false)
            return;

    }

    void OnClickYesButton()
    {
        Managers.UI.ClosePopupUI(this);

        _onClickYesButton?.Invoke();
    }

    void OnClickNoButton()
    {      
        _onClickNoButton?.Invoke();
        OnComplete();
    }
    void OnComplete()
    {
       
        Managers.UI.ClosePopupUI(this);
    }
}
