using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Calle : EnemyController
{
    int powerbuffturn = 0;
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.Defense;
            IntentionFigure = 12;
            powerbuffturn++;
        }
        else {
            int num = Random.Range(0, 100);

            if (num < 34)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 13;
            }
            else if (num < 67)
            {
                curIntention = Intention.AttackMany;
                IntentionFigure = 10;
                AttackNum = 2;
            }
            else if (num < 100)
            {
                if (powerbuffturn == 1)
                {
                    curIntention = Intention.DefenseBuff;
                    powerbuffturn = 0;
                }
                else
                {
                    curIntention = Intention.Defense;
                    powerbuffturn++;
                }
                IntentionFigure = 12;
            }
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
                    yield return new WaitForSeconds(0.31f);
                }
                break;
            case Intention.Defense:
                GetShield(IntentionFigure);
                break;
            case Intention.DefenseBuff:
                GetShield(IntentionFigure);
                GetPower(2);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
}
