using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Ivan : EnemyController
{
    public override bool Init()
    {
        base.Init();
        Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);

        GetHeal(0);
        GetEnvy(3);
        return true;
    }
    public void GetHeal(int value) {
        Buff buff = buffList.GetBuffName("재생");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            var effect = Managers.Resource.Instantiate("Effect/Buff");
            effect.transform.position = transform.position;
            buffList.Add(new HealBuff { Name = "재생", Value = value, controller = this, des = Define.Heal });
        }
        RefreshUI();
    }
    public void GetEnvy(int value) {
        Buff buff = buffList.GetBuffName("질투");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            var effect = Managers.Resource.Instantiate("Effect/Buff");
            effect.transform.position = transform.position;
            Buff envy = new EnvyBuff("질투", value,this,Define.Envy);
            buffList.Add(envy);
        }
        RefreshUI();
    }
    int state = 0;
    public override void SetIntention()
    {
        switch (state)
        {
            case 0:
                curIntention = Intention.Attack;
                IntentionFigure = 20;
                state++;
                break;
            case 1:
                curIntention = Intention.AttackMany;
                IntentionFigure = 10;
                AttackNum = 4;
                state++;
                break;
            case 2:
                curIntention = Intention.Attack;
                IntentionFigure = 45;
                state++;
                break;
            case 3:
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 10;
                state++;
                break;
            case 4:
                curIntention = Intention.Attack;
                IntentionFigure = 45;
                state++;
                break;
            case 5:
                curIntention = Intention.AttackMany;
                IntentionFigure = 10;
                state++;
                AttackNum = 3;
                break;
            case 6:
                curIntention = Intention.Defense;
                IntentionFigure = 45;
                state = 0;
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
                    yield return new WaitForSeconds(1f);
                }
                break;
            case Intention.DeBuff:
                battleScene.GetStress(2, false);
                battleScene._playerController.GetWeakness(2);
                Managers.Sound.Play(Define.Sound.Effect, "Effect/디버프", Managers.Game.EffectSound * 0.7f);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public override void Damaged(int value)
    {
        Buff buff = buffList.GetBuffName("취약");

        if (buff != null && buff.Value > 0) value += (int)(value * (Managers.Game.VulenrablePercent / 100f));
        int temp = value;
        value -= Shield;
        Shield -= temp;
        if (value < 0) value = 0;
        if (Shield < 0) Shield = 0;
        animator.SetTrigger("Stun");
        var effect = Managers.Resource.Instantiate("Effect/Hit");
        effect.transform.position = transform.position;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/피격", Managers.Game.EffectSound);
        StartCoroutine(DamageMaterial());
        CurHp -= value;
        if (CurHp <= 0)
        {
            CurHp = 0;
            // 죽음
            animator.SetTrigger("Death");
        }
        RefreshUI();
    }
    public void Sound1() { 
        Managers.Sound.Play(Define.Sound.Effect, "Effect/검1", Managers.Game.EffectSound);
    }
    public void Sound2()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/검2", Managers.Game.EffectSound);
    }
    public void Sound3()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/검3", Managers.Game.EffectSound);
    }
}
