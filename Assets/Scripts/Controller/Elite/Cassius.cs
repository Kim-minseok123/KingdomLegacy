using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Cassius : EnemyController
{
    int state = 0;
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.Nothing;
            IntentionFigure = 1;
        }
        else
        {
            switch (state)
            {
                case 0:
                    state++;
                    curIntention = Intention.AttackDebuff;
                    IntentionFigure = 14;
                    break;
                case 1:
                    state = 0;
                    curIntention = Intention.Attack;
                    IntentionFigure = 25;
                    break;
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
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene._playerController.GetWeakness(3);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
