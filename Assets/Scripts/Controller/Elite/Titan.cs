using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Titan : EnemyController
{
    int state = 0;
    public override void SetIntention()
    {
        switch (state)
        {
            case 0:
                state++;
                curIntention = Intention.DeBuff;
                IntentionFigure = 2;
                break;
            case 1:
                state = 0;
                curIntention = Intention.Attack;
                IntentionFigure = 20;
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
            case Intention.DeBuff:
                battleScene._playerController.GetPoisoning(IntentionFigure);
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
