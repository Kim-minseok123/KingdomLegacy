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
            curIntention = Define.Intention.Attack;
            IntentionFigure = 7;
        }
        else if (num < 70)
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
    public override void IntentionMotion()
    {
        switch (curIntention)
        {
            case Intention.Attack:
                break;
            case Intention.Buff:
                break;
            case Intention.Defense:
                break;   
        }
    }
}
