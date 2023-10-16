using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FriendAbility
{
    public void Use();
    public void Setting(CardData card);
    public void Die();
}

public class InfinitySword : FriendAbility
{
    PlayerController playerController;
    public void Use()
    {
        if (playerController == null)
            playerController = Managers.UI.FindPopup<UI_BattlePopup>()._playerController;
        playerController.GetShield(6);
        playerController.GetSwordGauge(1);
    }
    public void Setting(CardData card)
    {
        GameEvents.TurnEnd -= Use;
        GameEvents.TurnEnd += Use;
    }

    public void Die()
    {
        GameEvents.TurnEnd -= Use;
    }
}
public class Hod : FriendAbility
{
    PlayerController playerController;
    CardData card;
    public void Use()
    {
        if (playerController == null)
            playerController = Managers.UI.FindPopup<UI_BattlePopup>()._playerController;
        playerController.GetShield(card.shield);
    }
    public void Setting(CardData card)
    {
        GameEvents.TurnEnd -= Use;
        GameEvents.TurnEnd += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnEnd -= Use;
    }
}
public class CalieCarly : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;
    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        for (int i = 0; i < card.anyAttribute; i++)
            battlePopup.DrawCards(Managers.Data.Cards[119]);
    }
    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;

    }
}
public class Davidson : FriendAbility
{
    UI_BattlePopup battlePopup;

    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        battlePopup.HealMana(1);
    }
    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;

    }
}

public class Bedford : FriendAbility
{

    public void Use()
    {

    }
    public void Setting(CardData card)
    {

    }

    public void Die()
    {

    }
}
public class Grafton : FriendAbility
{

    CardData card;
    public void Use()
    {
        Managers.Game.Money += card.anyAttribute;
    }
    public void Setting(CardData card)
    {
        GameEvents.KillEnemy -= Use;
        GameEvents.KillEnemy += Use;
        this.card = card;
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}

public class Beaufort : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;
    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        battlePopup.DrawCards(3);
        battlePopup.ThrowCardSelect(1, 99);
    }
    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}
public class Devon : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;
    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        if (card.weakness != 0)
        {
            var Enemy = battlePopup._enemyList.Random().GetComponent<EnemyController>();
            Enemy.GetWeakness(card.weakness);
            Enemy.GetVulenrable(card.vulnerable);
        }
        else {
            foreach (GameObject go in battlePopup._enemyList) {
                go.GetComponent<EnemyController>().GetVulenrable(1);
            }
        }
        
    }

    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}
public class Casey : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;
    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        battlePopup.DrawCards(card.drawCard);

    }

    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}
public class Selmore : FriendAbility
{
    
    public void Use()
    {
        
    }
    public void Setting(CardData card)
    {
        
    }

    public void Die()
    {
        
    }
}
public class Rudger : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;
    int[] datas = new int[4];
    int[] datasPlus = new int[4];
    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        int value = 0;
        if (card.ID == 109)
            value = datas.Random();
        else
            value = datasPlus.Random();

        battlePopup.DrawCards(Managers.Data.Cards[value]);
    }

    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        datas[0] = 120;
        datasPlus[0] = 121;
        datas[1] = 122;
        datasPlus[1] = 123;
        datas[2] = 124;
        datasPlus[2] = 125;
        datas[3] = 126;
        datasPlus[3] = 127;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}
public class Chellish : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;
    
    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        foreach (GameObject go in battlePopup._enemyList)
        {
            go.GetComponent<EnemyController>().HalfShield();
        }
    }

    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}
public class Esmerilda : FriendAbility
{
    PlayerController player;
    CardData card;

    public void Use()
    {
        if (player == null)
            player = Managers.UI.FindPopup<UI_BattlePopup>()._playerController;
        player.GetPower(card.anyAttribute);
    }

    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}
public class Line : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;

    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        foreach (GameObject go in battlePopup._enemyList)
        {
            go.GetComponent<EnemyController>().Damaged(card.anyAttribute);
        }
    }

    public void Setting(CardData card)
    {
        GameEvents.LostHp -= Use;
        GameEvents.LostHp += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.LostHp -= Use;
    }
}
public class Bian : FriendAbility
{
    UI_BattlePopup battlePopup;
    CardData card;

    public void Use()
    {
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        battlePopup._enemyList.Random().GetComponent<EnemyController>().GetdePower(card.anyAttribute);
    }

    public void Setting(CardData card)
    {
        GameEvents.TurnStart -= Use;
        GameEvents.TurnStart += Use;
        this.card = card;
    }

    public void Die()
    {
        GameEvents.TurnStart -= Use;
    }
}