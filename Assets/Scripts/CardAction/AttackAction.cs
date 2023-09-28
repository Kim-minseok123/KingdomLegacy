using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null) {
        int Damage = 0;
        switch (card.ID) {
            case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 13: case 14:case 15:case 16: case 23: case 24:
                Damage = card.damage + player.Power;
                break;
            case 9: case 10:
                Damage = player.Shield + player.Power;
                break;
            case 11:
                Damage = card.damage + player.Power * 2;
                break;
            case 12:
                Damage = card.damage + player.Power * 5;
                break;
            case 17:
                Damage = card.damage + player.Power;
                for (int i = 0; i < 2; i++) {
                    player.AttackEnemy(Damage, enemy);
                }
                break;
            case 18:
                Damage = card.damage + player.Power;
                for (int i = 0; i < 3; i++)
                {
                    player.AttackEnemy(Damage, enemy);
                }
                break;
            case 19: case 20:
                    //적이 공격의도라면  취약 1 부여 및 공격 만큼 데미지
                break;
            case 21:
                Damage = player.infinitySword * 15 + player.Power;
                break;
            case 22:
                Damage = player.infinitySword * 20 + player.Power;
                break;
            case 25: case 26:
                Damage = card.damage + (int)(player.Power * 0.5);
                player.HealHp(Damage);
                break;
            case 27: case 28:
                foreach (UI_Card uicard in player._battleScene._handCardsUI) {
                    if (uicard._cardData.type != Define.CardType.Attack) { 
                        player._battleScene.ExitCard(uicard);
                    }
                }
                Damage = card.damage + player.Power;
                break;
            case 29: case 30:
                if(enemy != null)
                {
                    enemy.Shield = 0;
                }
                Damage = card.damage + player.Power;
                break;
        }

        player.AttackEnemy(Damage, enemy);
    }
}
