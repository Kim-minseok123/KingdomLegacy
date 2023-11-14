using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePowerAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 51: case 52:
                player.GetPower(card.increasePower);
                player.GetdePower(card.increasePower);
                break;
            case 67: case 68:
                //선택한 적이 공격할 의도라면 공격 2 획득
                if (enemy.curIntention == Define.Intention.Attack || enemy.curIntention == Define.Intention.AttackDebuff || enemy.curIntention == Define.Intention.AttackDefense)
                    player.GetPower(card.increasePower);
                break;
            case 89: case 90:
                player.GetPower(player.Power);
                break;
            case 119:
                player.GetPower(card.increasePower);
                player.GetdePower(card.increasePower);
                player.HealHp(1);
                break;
        }
    }
}
