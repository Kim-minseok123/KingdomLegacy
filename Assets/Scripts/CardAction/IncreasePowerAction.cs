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
                //������ ���� ������ �ǵ���� ���� 2 ȹ��
                break;
            case 89: case 90:
                player.GetPower(player.Power);
                break;
        }
    }
}
