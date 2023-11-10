using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Valiant : EnemyController
{
    public override bool Init()
    {
        base.Init();
        GetPower(5);
        return true;
    }
    int state = 0;
    public override void SetIntention()
    {
        switch (state)
        {
            case 0:
                state++;
                curIntention = Intention.Attack;
                IntentionFigure = 20;
                break;
            case 1:
                state = 0;
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 15;
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
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene.GetStress(2, false);
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
