using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Jerrold : EnemyController
{
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.DeBuff;
            IntentionFigure = 5;
        }
        else {
            int num = Random.Range(0, 100);

            if (num < 60)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 30;
            }
            else if (num < 40)
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
                GetPower(IntentionFigure);
                break;
            case Intention.DeBuff:
                battleScene._playerController.GetVulenrable(IntentionFigure); 
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
