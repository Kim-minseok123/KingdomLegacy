using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Item : UI_Base
{
    enum Images { 
        ItemImage,
    }
    public ItemData _itemData;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        if (_init == false)
            return;

        if (_itemData == null)
            return;

        //GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>($"Sprites/Item/{_itemData.ID}");

    }
    public void SetInfo(int id)
    {
        if (Managers.Data.Items.TryGetValue(id, out _itemData) == false)
        {
            Debug.Log($"Faild Load Card Data. Card id is {id}");
            return;
        }
        _itemData.ability.Setting();
        RefreshUI();
    }
}
