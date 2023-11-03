using System;
using System.Linq;
using Unity.Mathematics;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static Define;
using static Extension;
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
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp._playerController.HealHp(value);
        }
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
        //°ñµå Ãß°¡ È¹µæ.
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.isGoldPlusItem = true;
        //ÈÞ½Ä ¸øÇÏ°Ô
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
        //ÈÞ½Ä ¸øÇÏ°Ô
        Managers.Game.isRest = true;
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
        Managers.Game.isEnhance = true;
        Managers.Game.Mana += value;
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
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            foreach (GameObject obj in btp._enemyList)
            {
                obj.GetComponent<EnemyController>().GetPower(2);
            }
        }
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.Mana += value;
        //ÈÞ½Ä ¸øÇÏ°Ô
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
    }
}
public class Item7 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.GetStress(2);
        }
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.Mana += value;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
    }
}
public class Item8 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null && (Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.EliteEnemy || Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.Boss))
        {
            Managers.Game.Mana += value;
            GameEvents.BattleEnd -= BattleEnd;
            GameEvents.BattleEnd += BattleEnd;
        }
    }
    public void BattleEnd()
    {
        GameEvents.BattleEnd -= BattleEnd;
        Managers.Game.Mana -= value;
    }
    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item9 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.Mana += value;
        Managers.Game.isIntension = true;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item10 : ItemAbility
{
    public override void Use()
    {

    }
    public void Turn(int value)
    {
        if (value % 3 == 0) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if(btp != null)
            {
                btp.HealMana(1);
            }
        }
    }
    public override void Setting()
    {
        GameEvents.TurnValue -= Turn;
        GameEvents.TurnValue += Turn;
    }
}
public class Item11 : ItemAbility
{
    public override void Use()
    {
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.MaxHp += 7;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item12 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null) {
            btp._playerController.GetShield(value);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item13 : ItemAbility
{
    public override void Use()
    {
        
    }
    public CardData GetCard(CardData card) {
        Managers.Game.Money += value;
        return card;
    }

    public override void Setting()
    {
        GameEvents.GetCard -= GetCard;
        GameEvents.GetCard += GetCard;
    }
}
public class Item14 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null && btp._playerController.Shield == 0)
        {
            btp._playerController.GetShield(value);
        }
    }

    public override void Setting()
    {
        GameEvents.TurnEnd -= Use;
        GameEvents.TurnEnd += Use;
    }
}
public class Item15 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp._playerController.GetPower(value);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item16 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp._playerController.GetAgility(value);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item17 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp._playerController.HealHp(value);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item18 : ItemAbility
{
    public override void Use()
    {
        Managers.Game.CurHp += value;
        if(Managers.Game.CurHp >= Managers.Game.MaxHp)
            Managers.Game.CurHp = Managers.Game.MaxHp;
    }

    public override void Setting()
    {
        GameEvents.TakeRest -= Use;
        GameEvents.TakeRest += Use;
    }
}
public class Item19 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.HealMana(value);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}

public class Item20 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp._playerController.GetPower(value);
            foreach (GameObject obj in btp._enemyList)
                obj.GetComponent<EnemyController>().GetPower(1);
        }
    }

    public override void Setting()
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
    }
}
public class Item21 : ItemAbility
{
    public override void Use()
    {
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.MaxHp += 10;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item22 : ItemAbility
{
    public override void Use()
    {
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.MaxHp += 7;
        Managers.Game.CurHp = Managers.Game.MaxHp;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item23 : ItemAbility
{
    public override void Use()
    {
        if (Managers.Game.CurHp <= Managers.Game.MaxHp / 2) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null)
            {
                btp._playerController.HealHp(12);
            }
        }
    }

    public override void Setting()
    {
        GameEvents.BattleEnd -= Use;
        GameEvents.BattleEnd += Use;
    }
}
public class Item24 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.HealMana(1);
            btp.DrawCards(1);
        }
    }

    public override void Setting()
    {
        GameEvents.KillEnemy -= Use;
        GameEvents.KillEnemy += Use;
    }
}
public class Item25 : ItemAbility
{
    int useAttackCard = 0;
    public override void Use()
    {
        useAttackCard = 0;
    }
    public void UseCard(CardData card) {
        if (card.type == CardType.Attack) { 
            useAttackCard++;
        }
        if (useAttackCard == 3) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null) {
                btp._playerController.GetShield(10);
            }
            useAttackCard = 0;
        }
    }
    public override void Setting()
    {
        GameEvents.UseCard -= UseCard;
        GameEvents.UseCard += UseCard;
        GameEvents.TurnEnd -= Use;
        GameEvents.TurnEnd += Use;
    }
}
public class Item26 : ItemAbility
{
    
    public override void Use()
    {
        Managers.Game.MaxHp += 1;
    }

    public override void Setting()
    {
        GameEvents.BattleEnd -= Use;
        GameEvents.BattleEnd += Use;
    }
}
public class Item27 : ItemAbility
{
    int UseCardNum = 0;
    public override void Use()
    {
    }
    public void UseCard(CardData card) {
        UseCardNum++;
        if (UseCardNum == 4) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp !=null)
            {
                btp.DrawCards(1);
            }
            UseCardNum = 0;
        }
    }
    public override void Setting()
    {
        GameEvents.UseCard -= UseCard;
        GameEvents.UseCard += UseCard;
    }
}
public class Item28 : ItemAbility
{

    public override void Use()
    {
    }
    public CardData GetCard(CardData card)
    {
        if (card.ID % 2 == 1) {
            return Managers.Data.Cards[card.ID + 1];
        }
        return card;
    }
    public override void Setting()
    {
        GameEvents.GetCard -= GetCard;
        GameEvents.GetCard += GetCard;
    }
}
public class Item29 : ItemAbility
{

    public override void Use()
    {
    }
    public CardData GetCard(CardData card)
    {
        if (card.ID % 2 == 1)
        {
            return Managers.Data.Cards[card.ID + 1];
        }
        return card;
    }
    public override void Setting()
    {
        GameEvents.GetCard -= GetCard;
        GameEvents.GetCard += GetCard;
    }
}
public class Item30 : ItemAbility
{

    public override void Use()
    {
    }
    public CardData GetCard(CardData card)
    {
        if (card.ID % 2 == 1)
        {
            return Managers.Data.Cards[card.ID + 1];
        }
        return card;
    }
    public override void Setting()
    {
        GameEvents.GetCard -= GetCard;
        GameEvents.GetCard += GetCard;
    }
}
public class Item31 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null && Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.MinorEnemy)
        {
            foreach (var obj in btp._enemyList)
            {
                var controller = obj.GetComponent<EnemyController>();
                controller.Damaged((int)(controller.MaxHp * 0.2f));
            }
        }
    }
    
    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item32 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.DrawCards(1);
        }
    }

    public override void Setting()
    {
        GameEvents.LostHp -= Use;
        GameEvents.LostHp += Use;
    }
}
public class Item33 : ItemAbility
{
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
        Managers.Game.MaxHp += 14;
    }
}
public class Item34 : ItemAbility
{
    public override void Use()
    {
        
    }
    public void TurnValue(int value) {
        if (value == 7) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null )
            {
                for(int i = btp._enemyList.Count -1; i>=0; i--)
                {
                    var controller = btp._enemyList[i].GetComponent<EnemyController>();
                    controller.Damaged(100);
                }
            }
        }
    }

    public override void Setting()
    {
        GameEvents.TurnValue -= TurnValue;
        GameEvents.TurnValue += TurnValue;
    }
}
public class Item35 : ItemAbility
{
    public override void Use()
    {

    }
    public CardData GetCard(CardData card)
    {
        Managers.Game.CurHp += 7;
        if(Managers.Game.CurHp >= Managers.Game.MaxHp )
            Managers.Game.CurHp = Managers.Game.MaxHp;
        return card;
    }

    public override void Setting()
    {
        GameEvents.GetCard -= GetCard;
        GameEvents.GetCard += GetCard;
    }
}
public class Item36 : ItemAbility
{
    int useAttackCard = 0;
    public override void Use()
    {
        useAttackCard = 0;
    }
    public void UseCard(CardData card)
    {
        if (card.type == CardType.Attack)
        {
            useAttackCard++;
        }
        if (useAttackCard == 3)
        {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null)
            {
                btp._playerController.GetPower(1);
            }
            useAttackCard = 0;
        }
    }
    public override void Setting()
    {
        GameEvents.UseCard -= UseCard;
        GameEvents.UseCard += UseCard;
        GameEvents.TurnEnd -= Use;
        GameEvents.TurnEnd += Use;
    }
}
public class Item37 : ItemAbility
{

    public override void Use()
    {
        
    }
    public void UseCard(CardData card)
    {
        if (card.type == CardType.Friend)
        {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null)
            {
                btp._playerController.HealHp(2);
            }
        }
    }
    public override void Setting()
    {
        GameEvents.UseCard -= UseCard;
        GameEvents.UseCard += UseCard;
    }
}
public class Item38 : ItemAbility
{

    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.VulenrablePercent = 75;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item39 : ItemAbility
{

    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null) {
            btp._playerController.GetShield(6);
        }
    }

    public override void Setting()
    {
        GameEvents.ShuffleDeck -= Use;
        GameEvents.ShuffleDeck += Use;
    }
}
public class Item40 : ItemAbility
{

    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null && Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.Boss)
        {
            btp._playerController.HealHp(25);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item41 : ItemAbility
{
    public override void Use()
    {
        
    }
    public void TurnValue(int value) {
        if (value == 2) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null)
            {
                btp._playerController.GetShield(15);
            }
        }
    }

    public override void Setting()
    {
        GameEvents.TurnValue -= TurnValue;
        GameEvents.TurnValue += TurnValue;
    }
}
public class Item42 : ItemAbility
{
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
        Managers.Game.Money += 300;
    }
}
public class Item43 : ItemAbility
{
    public override void Use()
    {

    }
    public void TurnValue(int value)
    {
        if (value == 3)
        {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null)
            {
                btp._playerController.GetShield(20);
            }
        }
    }

    public override void Setting()
    {
        GameEvents.TurnValue -= TurnValue;
        GameEvents.TurnValue += TurnValue;
    }
}
public class Item44 : ItemAbility
{
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
        Managers.Game.isDiscount = true;
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
public class Item46 : ItemAbility
{
    public override void Use()
    {
        int rand = UnityEngine.Random.Range(1, 119);
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.DrawCards(Managers.Data.Cards[rand]);
        }
    }

    public override void Setting()
    {
        GameEvents.ExitCard -= Use;
        GameEvents.ExitCard += Use;
    }
}
public class Item47 : ItemAbility
{
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
        Managers.Game.isManaDisappear = true;
    }
}
public class Item48 : ItemAbility
{
    public override void Use()
    {
        int rand = UnityEngine.Random.Range(1, 119);
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            for(int i = btp._enemyList.Count -1; i >= 0; i --)
            {
                btp._enemyList[i].GetComponent<EnemyController>().Damaged(5);
            }
        }
    }

    public override void Setting()
    {
        GameEvents.ExitCard -= Use;
        GameEvents.ExitCard += Use;
    }
}
public class Item49 : ItemAbility
{
    int Turnvalue = 0;
    public override void Use()
    {
        
    }
    public void TurnValue(int value) {
        Turnvalue++;
        if (Turnvalue == 6) {
            UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
            if (btp != null)
            {
                btp._playerController.GetInviolable(1);
            }
        }
    }
    public override void Setting()
    {
        GameEvents.TurnValue -= TurnValue;
        GameEvents.TurnValue += TurnValue;
    }
}
public class Item50 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            foreach (var obj in btp._enemyList)
            {
                obj.GetComponent<EnemyController>().GetVulenrable(1);
            }
        }
    }
    
    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item51 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            for(int i = btp._enemyList.Count -1; i >= 0; i --)
            {
                btp._enemyList[i].GetComponent<EnemyController>().Damaged(3);
            }
        }
    }

    public override void Setting()
    {
        GameEvents.DrawCard -= Use;
        GameEvents.DrawCard += Use;
    }
}
public class Item52 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            var Rancard = Managers.Data.Cards.ElementAt(_rand.Next(91, 119)).Value;
            Rancard.mana = 0;
            btp.DrawCards(Rancard);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item53 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp._playerController.GetPower(1);
        }
    }

    public override void Setting()
    {
        GameEvents.LostHp -= Use;
        GameEvents.LostHp += Use;
    }
}
public class Item54 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.HealMana(1);
        }
    }

    public override void Setting()
    {
        GameEvents.LostHp -= Use;
        GameEvents.LostHp += Use;
    }
}
public class Item55 : ItemAbility
{
    public void GetItem() {
        GameEvents.GetItem -= GetItem;
        Managers.Game.MaxHp += 6;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }

    public override void Use()
    {
     
    }
}
public class Item56 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            btp.HealMana(1);
        }
    }

    public override void Setting()
    {
        GameEvents.ShuffleDeck -= Use;
        GameEvents.ShuffleDeck += Use;
    }
}
public class Item57 : ItemAbility
{
    public override void Use()
    {
        
    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.isResurrection = true;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item58 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.MaxHp += 20;
        Managers.Game.CurHp = Managers.Game.MaxHp;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item59 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.isChoice = true;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item60 : ItemAbility
{
    public override void Use()
    {

    }
    public void GetItem()
    {
        GameEvents.GetItem -= GetItem;
        Managers.Game.isDoublePoisoning = true;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item61 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            int rand = _rand.Next(0, btp._enemyList.Count);
            btp._enemyList[rand].GetComponent<EnemyController>().GetPoisoning(10);
        }
    }

    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}
public class Item62 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup btp = Managers.UI.FindPopup<UI_BattlePopup>();
        if (btp != null)
        {
            foreach (var obj in btp._enemyList)
            {
                obj.GetComponent<EnemyController>().GetPoisoning(2);
            }
        }
    }

    public override void Setting()
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
    }
}
public class Item63 : ItemAbility
{
    public override void Use()
    {
        Managers.Game.Money += 1;
    }

    public override void Setting()
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
    }
}
public class Item64 : ItemAbility
{
    public override void Use()
    {
    }
    public void GetItem() { 
        Managers.Game.Money += 2000;
        GameEvents.GetItem -= GetItem;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item65 : ItemAbility
{
    public override void Use()
    {
    }
    public void GetItem()
    {
        Managers.Game.isNonDebuff = true;
        GameEvents.GetItem -= GetItem;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item66 : ItemAbility
{
    public override void Use()
    {
    }
    public void GetItem()
    {
        Managers.Game.isDoubleItem = true;
        GameEvents.GetItem -= GetItem;
    }
    public override void Setting()
    {
        GameEvents.GetItem -= GetItem;
        GameEvents.GetItem += GetItem;
    }
}
public class Item67 : ItemAbility
{
    public override void Use()
    {
        UI_BattlePopup bpt = Managers.UI.FindPopup<UI_BattlePopup>();
        if (bpt != null)
        {
            bpt._playerController.GetPower(2);
        }
    }
    public override void Setting()
    {
        GameEvents.BattleStart -= Use;
        GameEvents.BattleStart += Use;
    }
}