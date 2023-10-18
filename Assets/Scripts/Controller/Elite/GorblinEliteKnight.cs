using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GorblinEliteKnight : EnemyController
{
    public override void SetIntention()
    {
        if (battleScene._curTurn % 2 == 1) {
            if (UnitNumber == 1 || UnitNumber == 3)
            {
                curIntention = Intention.DeBuff;
                IntentionFigure = 2;
            }
            else {
                curIntention = Intention.Attack;
                IntentionFigure = 10;
            }
        }
        else
        {
            if (UnitNumber == 1 || UnitNumber == 3)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 10;
            }
            else
            {
                curIntention = Intention.DeBuff;
                IntentionFigure = 2;
            }

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
            case Intention.DeBuff:
                battleScene.GetStress(IntentionFigure);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
