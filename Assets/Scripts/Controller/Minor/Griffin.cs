using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Griffin : EnemyController
{
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.DeBuff;
            IntentionFigure = 3;
        }
        else {
            int num = Random.Range(0, 100);

            if (num < 50)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 30;
            }
            else if (num < 100)
            {
                curIntention = Intention.Buff;
                IntentionFigure = 5;
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
            case Intention.Buff:
                GetPower(5);
                break;
            case Intention.DeBuff:
                battleScene._playerController.GetVulenrable(IntentionFigure);
                battleScene._playerController.GetBeDamaged(IntentionFigure);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
