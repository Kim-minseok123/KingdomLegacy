using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Valtter : EnemyController
{
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.DeBuff;
            IntentionFigure = 0;
        }
        else
        {
            int num = Random.Range(0, 100);

            if (num < 60)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 18;
            }
            else if (num < 40)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 25;
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
            case Intention.DeBuff:
                battleScene._playerController.GetRestraint(1);
                Managers.Sound.Play(Define.Sound.Effect, "Effect/디버프", Managers.Game.EffectSound * 0.7f);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public override void AttackAnim()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/전사", Managers.Game.EffectSound);
        base.AttackAnim();
    }
}
