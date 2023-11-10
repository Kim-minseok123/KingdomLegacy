using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Adelaar : EnemyController
{
    public override void SetIntention()
    {
        int num = Random.Range(0, 100);

        if (num < 50)
        {
            curIntention = Intention.Attack;
            IntentionFigure = 25;
        }
        else if (num < 100)
        {
            curIntention = Intention.AttackDefense;
            IntentionFigure = 12;
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
                GetShield(15);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public override void AttackAnim()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/นýป็", Managers.Game.EffectSound);
        base.AttackAnim();
    }
}
