using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Item : UI_Base
{
    enum Images { 
        ItemImage,
        ToolTipImage,
    }
    enum Texts { 
        ToolTipText,
    }
    public ItemData _itemData;
    public int type = 0;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        if (type == 1)
        {
            float x = transform.localPosition.x;
            while (x > 1800)
            {
                x -= 1800;
            }
            if (x > 1000)
            {
                GetImage((int)Images.ToolTipImage).gameObject.transform.localPosition = new Vector3(-175, -150, 0);
            }
            else
            {
                GetImage((int)Images.ToolTipImage).gameObject.transform.localPosition = new Vector3(175, -150, 0);

            }
            GetImage((int)Images.ItemImage).rectTransform.sizeDelta = new Vector2(50, 50);
        }
        else
        {

            if (transform.localPosition.x <= 0)
                GetImage((int)Images.ToolTipImage).gameObject.transform.localPosition = new Vector3(150, -170, 0);
            else
                GetImage((int)Images.ToolTipImage).gameObject.transform.localPosition = new Vector3(-150, -170, 0);
        }

        GetImage((int)Images.ToolTipImage).gameObject.SetActive(false);
        if (_itemData.rarity == Define.Rarity.Normal) {
            GetText((int)Texts.ToolTipText).text = "<color=#32CD32>" + _itemData.name + "</color>\n\n" + _itemData.description;
        }
        else if (_itemData.rarity == Define.Rarity.Rare) {
            GetText((int)Texts.ToolTipText).text = "<color=#1E90FF>" + _itemData.name + "</color>\n\n" + _itemData.description;

        }
        else if (_itemData.rarity == Define.Rarity.Unique) {
            GetText((int)Texts.ToolTipText).text = "<color=#FF00FF>" + _itemData.name + "</color>\n\n" + _itemData.description;
        }
        else if (_itemData.rarity == Define.Rarity.Legend) { 
            GetText((int)Texts.ToolTipText).text = "<color=#FF8C00>" + _itemData.name + "</color>\n\n" + _itemData.description;
        }
        if (type != 1) GetImage((int)Images.ItemImage).gameObject.BindEvent(GetItemClick);
        GetImage((int)Images.ItemImage).gameObject.BindEvent((go) => { TooltipOn(); },Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemImage).gameObject.BindEvent(TooltipOff,Define.UIEvent.PointerExit);

        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        if (_init == false)
            return;

        if (_itemData == null)
            return;

        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Item/{_itemData.ID}");

    }
    public void SetInfo(int id, int type)
    {
        if (Managers.Data.Items.TryGetValue(id, out _itemData) == false)
        {
            Debug.Log($"Faild Load Card Data. Card id is {id}");
            return;
        }
        this.type = type; 

        RefreshUI();
    }
    public virtual void GetItemClick()
    {
        if (type == 1) return;
        else if (type == 2)
        {
            Managers.UI.FindPopup<UI_ChooseClearItemPopup>().EndSelect();
        }
        else if (type == 3) {
            _itemData.ability.Setting();
            Managers.Game.Items.Add(_itemData.ID);
            Managers.UI.FindPopup<UI_MapPopup>().AddItem(_itemData.ID);

            GameEvents.OnGetItem();
            Managers.Game.SaveGame();
            Destroy(gameObject);
            return;
        }
        
        _itemData.ability.Setting();
        Managers.Game.Items.Add(_itemData.ID);
        Managers.UI.FindPopup<UI_MapPopup>().AddItem(_itemData.ID);

        GameEvents.OnGetItem();
        type = 1;
        GetImage((int)Images.ItemImage).gameObject.UnBindEvent(GetItemClick);
    }
    public void TooltipOn() {
        GetImage((int)Images.ToolTipImage).gameObject.SetActive(true);
    }
    public void TooltipOff() { 
        GetImage((int)Images.ToolTipImage).gameObject.SetActive(false);
    }
}
