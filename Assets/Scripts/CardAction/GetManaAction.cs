using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManaAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID)
        {
            case 15: case 16:
                if (enemy != null)
                {
                    if (enemy.Vulenerable > 0) { 
                        player._battleScene.HealMana(card.getMana);
                    }
                }
                break;
            case 77: case 78:
                player.Damaged(3);
                player._battleScene.HealMana(card.getMana);
                break;
        }
    }
}
