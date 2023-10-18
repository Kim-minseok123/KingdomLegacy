using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CorruptKnight : EnemyController
{
    public override void SetIntention()
    {
        if (battleScene._curTurn == 1)
        {
            curIntention = Intention.Buff;
            IntentionFigure = 0;
        }
        else
        {
            int num = Random.Range(0, 100);

            if (num < 50)
            {
                curIntention = Intention.Attack;
                IntentionFigure = 16;
            }
            else if (num < 100)
            {
                curIntention = Intention.AttackDebuff;
                IntentionFigure = 7;
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
            case Intention.Buff:
                GetShout(IntentionFigure);
                break;
            case Intention.AttackDebuff:
                AttackPlayer(IntentionFigure);
                battleScene._playerController.GetWeakness(2);
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public void GetShout(int value) {
        Buff buff = buffList.GetBuffName("함성");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new DePowerBuff { Name = "함성", Value = value, controller = this, des = Define.Shout });
        }
        else
        {
            buff.Value += value;
        }
        GameEvents.UseCard -= UseSkillCard;
        GameEvents.UseCard += UseSkillCard;
        RefreshUI();
    }
    public void UseSkillCard(CardData card) {
        if (card.type == CardType.Skill)
            GetPower(2);
    }
    public override void Death()
    {
        base.Death();
        GameEvents.UseCard -= UseSkillCard;
    }
}
