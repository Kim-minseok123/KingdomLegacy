using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Froce : EnemyController
{
    int state = 0; 
    public override void SetIntention()
    {
        switch (state) {
            case 0:
                curIntention = Intention.Nothing;
                IntentionFigure = 1;
                state = 1;
                break;
            case 1:
                curIntention = Intention.Attack;
                IntentionFigure = 25;
                state = 0;
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
            case Intention.Nothing:
                yield break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
