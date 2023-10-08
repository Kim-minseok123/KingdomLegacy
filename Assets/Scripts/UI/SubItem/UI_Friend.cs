using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Friend : UI_Base
{
    enum Images { 
        FriendImage,
    }
    enum Texts { 
        NameText,
    }
    public string Name;
    public FriendAbility FriendAbility;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        if (Name == "무한의 검")
        {
            RectTransform rectTransform = (RectTransform)GetImage((int)Images.FriendImage).transform;
            rectTransform.sizeDelta = new Vector2(40.9f, 150);
            GetImage((int)Images.FriendImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Friend/{name}");
        }
        else
            GetImage((int)Images.FriendImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Friend/{name}_0");
        gameObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load($"Animation/Friend/{Name}/{Name}", typeof(RuntimeAnimatorController)));
        GetText((int)Texts.NameText).text = Name;
        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        if (_init == false)
            return;

        if (FriendAbility == null)
            return;
    }
    public void SetInfo(string name, FriendAbility ability, CardData card)
    {
        Name = name;
        FriendAbility = ability;
        FriendAbility.Setting(card);
    }
}
