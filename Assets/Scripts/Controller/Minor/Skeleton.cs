using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Skeleton : EnemyController
{

    public override void SetIntention()
    {
        curIntention = Intention.AttackDebuff;
        IntentionFigure = 18;
        base.SetIntention();
    }
    public override IEnumerator IntentionMotion()
    {
        switch (curIntention)
        {
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene._playerController.GetWeakness(1);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
