using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        CardManaImage,
    }
    public CardData _cardData;
    public bool _isUseCard = true;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        Image uiImage = GetComponent<Image>();
        uiImage.material = new Material(uiImage.material);
        GetImage((int)Images.CardImage).material = uiImage.material;
        GetImage((int)Images.CardBackGroundImage).material = uiImage.material;
        GetImage((int)Images.DontUseCardImage).material = uiImage.material;
        GetImage((int)Images.CardManaImage).material = uiImage.material;

        RefreshUI();

        return true;
    }
    void RefreshUI() {
        if (_init == false)
            return;

        if(_cardData == null)
            return;

        GetText((int)Texts.CardNameText).text = _cardData.name;
        if (_cardData.ID == 33 || _cardData. ID == 34) { 
            GetText((int)Texts.CardManaText).text = "x";
        }
        else
        {
        GetText((int)Texts.CardManaText).text = _cardData.mana.ToString();

        }
        GetText((int)Texts.CardContentsText).text = _cardData.FormattedDescription;
        

        if (_cardData.type == Define.CardType.Attack)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/AttackCard");
        else if(_cardData.type == Define.CardType.Skill)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/SkillCard");
        else if(_cardData.type == Define.CardType.Friend)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/FriendCard");

        //GetImage((int)Images.CardImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Card/{_cardData.ID}");

    }
    public UI_Card SetInfo(CardData id) {
        _cardData = (CardData)id.Clone();
        RefreshUI();
        return this;
    }
    public void CheckManaUseCard(int curMana) {
        if (_cardData.mana <= curMana && _cardData.cardCondition.isUsable())
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
    public void BurnFade() { 
        GetText(0).gameObject.SetActive(false);
        GetText(1).gameObject.SetActive(false);
        GetText(2).gameObject.SetActive(false);
        StartCoroutine(Burn());
    }
    IEnumerator Burn() { 
        Material material = GetComponent<Image>().material;
        material.EnableKeyword("FADE_ON");
        float value = -0.1f;
        while (value <= 1f) {
            value += 0.03f;
            material.SetFloat("_FadeAmount", value);
            yield return null;
        }
        material.EnableKeyword("FADE_ON");
    }
}
