using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Gorblin : EnemyController
{
    int turnNumber = 0; 
    public override void SetIntention()
    {
        switch (UnitNumber) {
            case 1:
                curIntention = Intention.Attack;
                IntentionFigure = 6;
                break;
            case 2:
                if (battleScene._enemyList.Count < 2)
                {
                    curIntention = Intention.Attack;
                    IntentionFigure = 10;
                }
                else {
                    curIntention = Intention.Defense;
                    IntentionFigure = 10;
                }
                break;
            case 3:
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 5;
                break;
            case 4:
                if (turnNumber == 0)
                {
                    IntentionFigure = 2;
                    curIntention = Intention.Nothing;
                    turnNumber = 2;
                }
                else if (turnNumber == 1)
                {
                    IntentionFigure = 30;
                    curIntention = Intention.Attack;
                    turnNumber--;
                }
                else if (turnNumber <= 2) {
                    IntentionFigure--;
                    curIntention = Intention.Nothing;
                    turnNumber--;
                }
                break;
        }

        base.SetIntention();
    }
    public override IEnumerator IntentionMotion()
    {
        switch (curIntention)
        {
            case Intention.Attack:
                AttackPlayer(IntentionFigure);
                break;
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene._playerController.GetWeakness(1);
                break;
            case Intention.Defense:
                battleScene._enemyList.GetRandom().GetComponent<EnemyController>().GetShield(IntentionFigure);
                break;
            case Intention.Nothing:
                yield break;
        }
        StartCoroutine(base.IntentionMotion());
    }
}
