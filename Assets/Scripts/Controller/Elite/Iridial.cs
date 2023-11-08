using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Iridial : EnemyController
{
    public override void SetIntention()
    {
        curIntention = Intention.AttackDebuff;
        IntentionFigure = 10;

        base.SetIntention();
    }
    public override IEnumerator IntentionMotion()
    {
        switch (curIntention)
        {
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene.GetStress(2, false);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
