using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Serilda : EnemyController
{
    int state = 0;
    public override void SetIntention()
    {
        switch (state)
        {
            case 0:
                curIntention = Intention.AttackMany;
                IntentionFigure = 7;
                AttackNum = 2;
                state = 1;
                break;
            case 1:
                curIntention = Intention.Buff;
                IntentionFigure = 5;
                state = 0;
                break;
        }
        base.SetIntention();
    }
    public override IEnumerator IntentionMotion()
    {
        switch (curIntention)
        {
            case Intention.AttackMany:
                for (int i = 0; i < AttackNum; i++)
                {
                    AttackPlayer(IntentionFigure);
                    yield return new WaitForSeconds(0.7f);
                }
                break;
            case Intention.Buff:
                GetPower(IntentionFigure);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
