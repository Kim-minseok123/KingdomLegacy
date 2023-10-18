using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GorblinKnight : EnemyController
{
    public override void SetIntention()
    {
        int num = Random.Range(0, 100);

        if (num < 45)
        {
            curIntention = Intention.Attack;
            IntentionFigure = 12;
        }
        else if (num < 80)
        {
            curIntention = Intention.AttackDefense;
            IntentionFigure = 8;
        }
        else if (num < 100)
        {
            curIntention = Intention.DefenseBuff;
            IntentionFigure = 4;
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
                GetShield(5);
                break;
            case Intention.DefenseBuff:
                GetShield(7);
                GetPower(IntentionFigure);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
