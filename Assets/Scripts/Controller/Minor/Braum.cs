using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Braum : EnemyController
{
    public override bool Init()
    {
        base.Init();
        GetArmor(20);
        return true;
    }
    public override void Damaged(int value)
    {
        Buff buff = buffList.GetBuffName("Ãë¾à");

        if (buff != null && buff.Value > 0) value += (int)(value * (Managers.Game.VulenrablePercent / 100f));
        buff = buffList.GetBuffName("ÆÇ±Ý°©¿Ê");

        int temp = value;
        value -= Shield;
        Shield -= temp;
        if (value < 0) value = 0;
        if (Shield < 0)
        {
            Shield = 0;
            if (buff != null && value > 0)
            {
                buff.Value -= 1;
                if (buff.Value <= 0)
                    RemoveBuff(buff);
            }
        }

        StartCoroutine(DamageMaterial());
        CurHp -= value;
        if (CurHp <= 0)
        {
            CurHp = 0;
            // Á×À½
            animator.SetTrigger("Death");
        }
        RefreshUI();
    }

    public override void SetIntention()
    {
        int num = Random.Range(0, 100);

        if (num < 40)
        {
            curIntention = Intention.AttackMany;
            IntentionFigure = 8;
            AttackNum = 2;
        }
        else if (num < 80)
        {
            curIntention = Intention.Attack;
            IntentionFigure = 14;
        }
        else if (num < 20)
        {
            curIntention = Intention.AttackDebuff;
            IntentionFigure = 25;
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
                battleScene._playerController.GetWeakness(1);
                AttackPlayer(IntentionFigure);
                break;
            case Intention.AttackMany:
                for (int i = 0; i < AttackNum; i++)
                {
                    AttackPlayer(IntentionFigure);
                    yield return new WaitForSeconds(0.31f);
                }
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public bool isAttackMode = true;
    public void GetArmor(int value)
    {
        Buff buff = buffList.GetBuffName("ÆÇ±Ý°©¿Ê");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new ArmorBuff { Name = "ÆÇ±Ý°©¿Ê", Value = value, controller = this, des = Define.Armor });
        }
        RefreshUI();
    }
}
