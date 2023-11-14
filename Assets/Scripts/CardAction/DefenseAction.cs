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
            case 49:case 50: case 59: case 60: case 63: case 64: case 73: case 74: case 87: case 88:
                Shield = card.shield;
                break;
            case 53: case 54:
                Shield = card.shield ;
                player._battleScene.ThrowCardSelect(1, 1, card, player, enemy, Shield);
                return;
            case 55: case 56:
                Shield = player._battleScene._throwCards.Count;
                break;
            case 83: case 84:
                //무한의 검 소환
                FriendAbility Infinity = new InfinitySword();
                player._battleScene.MakeFriend("무한의 검", Infinity,card);
                return;
            case 85: case 86:
                Shield = player.Shield;
                break;

        }
        player.GetShield(Shield);
    }
}
