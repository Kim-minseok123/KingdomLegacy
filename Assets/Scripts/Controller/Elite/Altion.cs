using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Altion : EnemyController
{
    public override void SetIntention()
    {
        curIntention = Intention.AttackMany;
        IntentionFigure = 10;
        AttackNum = 2;

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
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public override void AttackAnim()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/±Ã¼ö", Managers.Game.EffectSound);
        base.AttackAnim();
    }
}
