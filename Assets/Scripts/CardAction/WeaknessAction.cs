using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 35: case 36:
                enemy.GetWeakness(card.weakness);
                break;
        }
    }
}
