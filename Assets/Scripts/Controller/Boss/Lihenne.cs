using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Lihenne : EnemyController
{
    int state = 0;
    public override void SetIntention()
    {
        switch (state) {
            case 0:
                curIntention = Intention.DeBuff;
                IntentionFigure = 6;
                state++;
                break;
            case 1:
                curIntention = Intention.AttackMany;
                IntentionFigure = 8;
                AttackNum = 2;
                state++;
                break;
            case 2:
                curIntention = Intention.DefenseBuff;
                IntentionFigure = 12;
                state++;
                break;
            case 3:
                curIntention = Intention.AttackMany;
                IntentionFigure = 8;
                AttackNum = 2;
                state++;
                break;
            case 4:
                curIntention = Intention.DefenseBuff;
                IntentionFigure = 12;
                state++;
                break;
            case 5:
                curIntention = Intention.Attack;
                IntentionFigure = 50;
                state++;
                AttackNum = 1;
                break;
            case 6:
                curIntention = Intention.Buff;
                IntentionFigure = 70;
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
                yield return StartCoroutine(Attack(IntentionFigure, 1));
                break;
            case Intention.Buff:
                CurHp = Math.Min(MaxHp, CurHp + IntentionFigure);
                break;
            case Intention.DefenseBuff:
                GetShield(IntentionFigure);
                GetPower(5);
                break;
            case Intention.AttackMany:
                yield return StartCoroutine(Attack(IntentionFigure, AttackNum));
                break;
            case Intention.DeBuff:
                battleScene.GetDizziness(6);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    
    IEnumerator Attack(int Damage,int num) {
        Buff buff = buffList.GetBuffName("Èû");
        if (buff != null)
            Damage += buffList.GetBuffName("Èû").Value;
        buff = buffList.GetBuffName("¾àÈ­");
        if (buff != null && buff.Value > 0)
            Damage = (int)(Damage * 0.75f);

        for(int i = 0; i < AttackNum; i ++)
        {
            animator.SetTrigger("Attack");
            battleScene._playerController.Damaged(Damage);
            yield return new WaitForSeconds(1.7f);
        }
    }
}
