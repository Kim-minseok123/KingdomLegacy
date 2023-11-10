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
