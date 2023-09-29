using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID)
        {
            case 31: case 32: case 37: case 38:
                enemy.GetPoisoning(card.poisoning);
                break;
        }
    }
}
