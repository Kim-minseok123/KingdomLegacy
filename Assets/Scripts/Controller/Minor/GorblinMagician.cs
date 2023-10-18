using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GorblinMagician : EnemyController
{
    private void Start()
    {
        GetBarrier(2);

    }
    public override void Damaged(int value) {
        Buff buff = buffList.GetBuffName("배리어");
        if (buff != null)
        {
            value = (int)(value * 0.75f);
            buff.Value--;
            if (buff.Value == 0)
                buffList.Remove(buff);
        }
        base.Damaged(value);
    }
    public void GetBarrier(int value) {
        Buff buff = buffList.GetBuffName("배리어");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new Buff { Name = "배리어", Value = value, controller = this, des = Define.Barrier });
        }
        else
        {
            buff.Value = value;
        }
        //이펙트 추가
        RefreshUI();
    }
    public override void SetIntention()
    {
        Buff Barrier = buffList.GetBuffName("배리어");
        if (Barrier == null && battleScene._curTurn % 2 == 0) {
            curIntention = Intention.Buff;
            IntentionFigure = 2;
            base.SetIntention();
            return;
        }
        int num = Random.Range(0, 100);

        if (num < 35)
        {
            curIntention = Intention.Attack;
            IntentionFigure = 6;
        }
        else if (num < 35)
        {
            curIntention = Intention.AttackMany;
            IntentionFigure = 1;
            AttackNum = 4;
        }
        else if (num < 30)
        {
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
            case Intention.AttackMany:
                for (int i = 0; i < AttackNum; i++)
                {
                    AttackPlayer(IntentionFigure);
                    yield return new WaitForSeconds(0.3f);
                }
                break;
            case Intention.Buff:
                if (IntentionFigure == 2)
                {
                    GetBarrier(IntentionFigure);
                }
                else { 
                    GetPower(IntentionFigure);
                }
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
