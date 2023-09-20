using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null) {
        int Damage = 0;
        switch (card.ID) {
            case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8:
                Damage = card.damage;
                break;
        }

        player.AttackEnemy(Damage, enemy);
    }
}
