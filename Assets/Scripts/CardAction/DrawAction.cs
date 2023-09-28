using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 13: case 14: case 15: case 16:
                player._battleScene.DrawCards(card.drawCard);
                break;
            
        }
    }
}
