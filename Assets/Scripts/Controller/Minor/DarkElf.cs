using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DarkElf : EnemyController
{
    //다크 엘프의 행동 패턴  
    public override void SetIntention() {
        int num = Random.Range(0, 100);

        if (num < 50)
        {
            curIntention = Intention.Attack;
            IntentionFigure = 99;
        }
        else if (num < 80)
        {
            curIntention = Intention.Defense;
            IntentionFigure = 6;
        }
        else if (num < 100) {
            curIntention = Intention.Buff;
            IntentionFigure = 5;
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
            case Intention.Buff:
                GetPower(IntentionFigure);
                Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);

                break;
            case Intention.Defense:
                GetShield(IntentionFigure);
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
