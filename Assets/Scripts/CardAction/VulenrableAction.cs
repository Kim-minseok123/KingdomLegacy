using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulenrableAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 3: case 4:
                var Enemys = Managers.UI.FindPopup<UI_BattlePopup>()._enemyList;
                foreach (GameObject go in Enemys) {
                    go.GetComponent<EnemyController>().GetVulenrable(card.vulnerable);
                }
                break;
            case 35: case 36:
                enemy.GetVulenrable(card.vulnerable);
                break;
        }
    }
}
