using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class RetiredKnight : EnemyController
{
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.DeBuff;
            IntentionFigure = 1;
        }
        else
        {
            int num = Random.Range(0, 100);

            if (num < 40)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 20;
            }
            else if (num < 70)
            {
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 11;
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
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene._playerController.GetVulenrable(2);
                break;
            case Intention.Buff:
                GetPower(IntentionFigure);
                battleScene._playerController.GetWeakness(3);
                break;
            case Intention.DeBuff:
                battleScene._playerController.GetConfusion(1);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
