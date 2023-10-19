using System;
using static Define;

public abstract class ItemAbility 
{
    protected int value;
    protected int frequency;
    public void Init(ItemData itemData) {
        value = itemData.value;
        frequency = itemData.frequency;
    }
    public abstract void Use();
    public abstract void Setting();
}
public static class ItemAbilityFactory
{
    public static ItemAbility CreateAbility(ItemData itemData) {
        string calssName = "Item" + itemData.ID.ToString();
        Type type = Type.GetType(calssName);

        if (type != null) {
            var item = (ItemAbility)Activator.CreateInstance(type);
            item.Init(itemData);
            return item;
        }

        return null;
    }
}

public class Item1 : ItemAbility {
    public override void Use()
    {
        //ü�� ȸ�� value ��ŭ
    }
    public override void Setting()
    {
        GameEvents.BattleEnd -= Use;
        GameEvents.BattleEnd += Use;
    }
}
public class Item2 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.DrawCards(value);
        }
    }
    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item3 : ItemAbility
{
    public override void Use()
    {
        //��� �߰� ȹ��.
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.isGoldPlusItem = true;
        //�޽� ���ϰ�
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item4 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem() {
        GameEvents.GetItem -= GetItem;
        Managers.Game.Mana += value;
        //�޽� ���ϰ�
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item5 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.Mana += value;
        //�޽� ���ϰ�
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item6 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.Mana += value;
        //�޽� ���ϰ�
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item45 :ItemAbility{
    public override void Use()
    {
        
    }

    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.isPreservation = true;
    }

}