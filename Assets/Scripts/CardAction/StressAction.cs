using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID)
        {
            case 43: case 44: case 73: case 74:
                player._battleScene.GetStress(card.disturbance);
                break;
        }
    }
}
