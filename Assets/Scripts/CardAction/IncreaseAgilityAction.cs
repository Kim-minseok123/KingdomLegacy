using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAgilityAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 57: case 58:
                player.GetAgility(card.increaseAgility);
                break;
        }
    }
}
