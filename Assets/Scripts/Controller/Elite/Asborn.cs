using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Asborn : EnemyController
{
    int state = 0;
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.DeBuff;
            IntentionFigure = 6;
        }
        else
        {

            switch (state) {
                case 0:
                    state++;
                    curIntention = Intention.AttackDefense;
                    IntentionFigure = 12;
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
            case Intention.AttackDefense:
                AttackPlayer(IntentionFigure);
                GetShield(IntentionFigure);
                break;
            case Intention.DeBuff:
                battleScene._playerController.GetAgility(-IntentionFigure);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
