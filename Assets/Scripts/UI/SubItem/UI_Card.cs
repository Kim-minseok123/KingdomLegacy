using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Card : UI_Base
{
    enum Texts {
        CardNameText,
        CardContentsText,
        CardManaText
    }
    enum Images { 
        CardImage,
        CardBackGroundImage,
        DontUseCardImage,
    }
    public CardData _cardData;
    public bool _isUseCard = true;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        RefreshUI();

        return true;
    }
    void RefreshUI() {
        if (_init == false)
            return;

        if(_cardData == null)
            return;

        GetText((int)Texts.CardNameText).text = _cardData.name;
        GetText((int)Texts.CardManaText).text = _cardData.mana.ToString();
        GetText((int)Texts.CardContentsText).text = _cardData.description;

        if (_cardData.type == Define.CardType.Attack)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/AttackCard");
        else if(_cardData.type == Define.CardType.Skill)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/SkillCard");
        else if(_cardData.type == Define.CardType.Friend)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/FriendCard");

        //GetImage((int)Images.CardImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Card/{_cardData.ID}");

    }
    public UI_Card SetInfo(int id) {
        if (Managers.Data.Cards.TryGetValue(id, out _cardData) == false) {
            Debug.Log($"Faild Load Card Data. Card id is {id}");
            return null;
        }
        RefreshUI();
        return this;
    }
    public void CheckManaUseCard(int curMana) {
        if (_cardData.mana <= curMana)
            _isUseCard = true;
        else
            _isUseCard = false;
    }     
    public void Update()
    {
        if (_isUseCard)
        {
            GetImage((int)Images.DontUseCardImage).gameObject.SetActive(false);
        }
        else { 
            GetImage((int)Images.DontUseCardImage).gameObject.SetActive(true);
        }
    }
}
