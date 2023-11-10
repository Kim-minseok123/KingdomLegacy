using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DeathTiner : EnemyController
{
    int state = 0;
    public override void SetIntention()
    {
        
        switch (state)
        {
            case 0:
                state++;
                curIntention = Intention.Attack;
                IntentionFigure = 15;
                break;
            case 1:
                state++;
                curIntention = Intention.DeBuff;
                IntentionFigure = 3;
                break;
            case 2:
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 10;
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
                battleScene._playerController.GetVulenrable(2);
                Managers.Sound.Play(Define.Sound.Effect, "Effect/디버프", Managers.Game.EffectSound * 0.7f);
                break;
            case Intention.DeBuff:
                battleScene._playerController.GetGag(IntentionFigure);
                Managers.Sound.Play(Define.Sound.Effect, "Effect/디버프", Managers.Game.EffectSound * 0.7f);

                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public override void AttackAnim()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/궁수", Managers.Game.EffectSound);
        base.AttackAnim();
    }
}
