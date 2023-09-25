using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        int Shield = 0;
        switch (card.ID)
        {
            case 49:case 50:
                Shield = card.shield;
                break;
        }
        player.GetShield(Shield);
    }
}
