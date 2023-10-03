using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzinessAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 47: case 48:
                player._battleScene.GetDizziness(card.disturbance);
                break;
        }
    }
}
