using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NonBattleCard : UI_Base
{
    string[] CardDesToolTip = new string[15] {"약화", "취약","힘","민첩","중독","방어도","힘 감소","무한의 검","방어막","소멸","보존","조건","텐션","소환","드로우"};
    int StartIndex = 20036;
    List<int> TooltipIndex = new List<int>();
    List<GameObject> ToolTip = new List<GameObject>(); 
    enum Texts
    {
        CardNameText,
        CardContentsText,
        CardManaText
    }
    enum Images
    {
        CardImage,
        CardBackGroundImage,
        CardManaImage,
        CardRarity,
    }
    enum Transforms {
        ToolTipTransform,
    }
    public CardData _cardData;
    public Transform pos;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        foreach (var name in CardDesToolTip) {
            if (_cardData.description.Contains(name)) {
                TooltipIndex.Add(StartIndex);
            }
            StartIndex++;
        }

        gameObject.BindEvent(ClickCard);
        gameObject.BindEvent((go)=> { ToolTipOn(); }, Define.UIEvent.PointerEnter);
        gameObject.BindEvent(ToolTipOff,Define.UIEvent.PointerExit);


        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        if (_init == false)
            return;

        if (_cardData == null)
            return;

        GetText((int)Texts.CardNameText).text = _cardData.name;
        if (_cardData.ID == 33 || _cardData.ID == 34)
        {
            GetText((int)Texts.CardManaText).text = "x";
        }
        else
        {
            GetText((int)Texts.CardManaText).text = _cardData.mana.ToString();

        }
        GetText((int)Texts.CardContentsText).text = _cardData.FormattedDescription;


        if (_cardData.type == Define.CardType.Attack)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/AttackCard");
        else if (_cardData.type == Define.CardType.Skill)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/SkillCard");
        else if (_cardData.type == Define.CardType.Friend)
            GetImage((int)Images.CardBackGroundImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/FriendCard");

        if (_cardData.rarity == Define.CardRarity.Normal)
        {
            GetImage((int)Images.CardRarity).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/Normal");
        }
        else if (_cardData.rarity == Define.CardRarity.Rare)
        {
            GetImage((int)Images.CardRarity).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/Rare");
        }
        else if (_cardData.rarity == Define.CardRarity.Unique)
        {
            GetImage((int)Images.CardRarity).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/Unique");
        }
        else if (_cardData.rarity == Define.CardRarity.Legend)
        {
            GetImage((int)Images.CardRarity).sprite = Managers.Resource.Load<Sprite>("Sprites/Card/Legend");
        }

        //GetImage((int)Images.CardImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Card/{_cardData.ID}");
    }
    public void SetInfo(int id) {
        if (Managers.Data.Cards.TryGetValue(id, out CardData card) == true)
            _cardData = (CardData)card.Clone();
    }

    public virtual void ClickCard() {
        return;
    }
    public void ToolTipOn()
    {
        for (int i = 0; i < TooltipIndex.Count; i++)
        {
            var tooltip = Managers.Resource.Instantiate("UI/SubItem/UI_ToolTip", transform);
            ToolTip.Add(tooltip);
            Vector3 temp = pos.localPosition;
            temp += new Vector3(0, i * -105f, 0);
            
            tooltip.GetComponent<UI_ToolTip>().SetInfo(TooltipIndex[i], temp);
        }
    }
    public void ToolTipOff() {
        for (int i = ToolTip.Count - 1; i >= 0; i--) {
            Managers.Resource.Destroy(ToolTip[i]);
        }
        ToolTip.Clear();
    }
}
