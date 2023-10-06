using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 35: case 36:
                enemy.GetWeakness(card.weakness);
                break;
            case 61: case 62: case 69: case 70:
                var Enemys = Managers.UI.FindPopup<UI_BattlePopup>()._enemyList;
                foreach (GameObject go in Enemys)
                {
                    go.GetComponent<EnemyController>().GetWeakness(card.weakness);
                }
                break;
        }
    }
}
