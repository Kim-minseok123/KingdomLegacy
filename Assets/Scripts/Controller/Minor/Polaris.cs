using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Polaris : EnemyController
{
    public void GetLightShield(int value) {
        Buff buff = buffList.GetBuffName("빛의방패");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            var effect = Managers.Resource.Instantiate("Effect/Buff");
            effect.transform.position = transform.position;
            IsShield = true;
            buffList.Add(new LightShield { Name = "빛의방패", Value = value, controller = this, des = Define.LightShield });
        }
        RefreshUI();
    }
    public override bool Init()
    {
        base.Init();
        Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);

        GetLightShield(0);
        GetShield(200);
        return true;
    }
    int state = 0;
    public override void SetIntention()
    {
        switch (state) {
            case 0:
                state++;
                curIntention = Intention.Defense;
                IntentionFigure = 30;
                break;
            case 1:
                state++;
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 9;
                break;
            case 2:
                state++;
                curIntention = Intention.AttackMany;
                IntentionFigure = 9;
                AttackNum = 2;
                break;
            case 3:
                state++;
                curIntention = Intention.AttackDefense;
                IntentionFigure = 9;
                break;
            case 4:
                state = 0;
                curIntention = Intention.Attack;
                IntentionFigure = 12;
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
            case Intention.Defense:
                GetShield(IntentionFigure);
                break;
            case Intention.AttackMany:
                for (int i = 0; i < AttackNum; i++)
                {
                    AttackPlayer(IntentionFigure);
                    yield return new WaitForSeconds(0.7f);
                }
                break;
            case Intention.AttackDefense:
                AttackPlayer(IntentionFigure);
                GetShield(10);
                break;
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene._playerController.GetBeDamaged(5);
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
