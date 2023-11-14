using Unity.VisualScripting;

public class AttackAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null) {
        int Damage = 0;
        switch (card.ID) {
            case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 13: case 14:case 15:case 16: case 35: case 36: case 41: case 42: case 43: case 44: case 47: case 48: case 122: case 123: case 126: case 127:
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
                player._battleScene.ManyTimesAttack(player, 3, Damage, false, enemy);
                return;
            case 18:
                Damage = card.damage + player.Power;
                player._battleScene.ManyTimesAttack(player, 4, Damage, false, enemy);
                return;
            case 19: case 20:
                //적이 공격의도라면  취약 1 부여 및 공격 만큼 데미지
                if (enemy.curIntention == Define.Intention.Attack || enemy.curIntention == Define.Intention.AttackDebuff || enemy.curIntention == Define.Intention.AttackDefense)
                {
                    enemy.GetVulenrable(1);
                    Damage = enemy.IntentionFigure + player.Power;
                }
                break;
            case 21:
                Damage = player.infinitySword * 15 + player.Power;
                player.ResetSwordGauge();
                break;
            case 22:
                Damage = player.infinitySword * 20 + player.Power;
                player.ResetSwordGauge();
                break;
            case 23:
                Damage = card.damage + player.Power;
                card.damage = 7;
                break;
            case 24:
                Damage = card.damage + player.Power;
                card.damage = 10;
                break;
            case 25: case 26:
                Damage = card.damage + (int)(player.Power * 0.5);
                player.AttackEnemy(Damage, true, enemy);
                return;
            case 27: case 28:
                for (int i = player._battleScene._handCardsUI.Count - 1; i >= 0; i--) {
                    if (player._battleScene._handCardsUI[i]._cardData.type != Define.CardType.Attack)
                    {
                        player._battleScene.ExitCard(player._battleScene._handCardsUI[i]);
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
            case 31: case 32:
                Damage = player._battleScene._curTurnUseCard * card.damage + player.Power;
                break;
            case 33: case 34:
                var num = player._battleScene._curMana;
                player._battleScene.ManyTimesAttack(player, num, player.Power + card.damage, true, enemy);
                return;
            case 37: case 38:
                player.Damaged(2);
                Damage = card.damage + player.Power;
                break;
            case 39: case 40:
                //카드 한장 버리는거 구현
                Damage = card.damage + player.Power;
                player._battleScene.ThrowCardSelect(1, 0, card, player,enemy, Damage);
                return;
            case 45:
                Damage = card.damage + player.Power;
                card.damage += 8;
                break;
            case 46:
                Damage = card.damage  + player.Power;
                card.damage += 12;
                break;
            case 81: case 82:
                int half = Managers.Game.CurHp / 3;
                Damage = (int)(half * 1.5) + player.Power;
                Managers.Game.CurHp -= half;
                player.RefreshUI();
                break;
        }

        player.AttackEnemy(Damage, enemy);
    }
}
