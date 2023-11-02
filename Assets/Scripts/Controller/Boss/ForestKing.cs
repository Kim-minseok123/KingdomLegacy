using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ForestKing : EnemyController
{
    public override bool Init()
    {
        base.Init();
        GetAttackMode(40);
        return true;
    }
    public override void Damaged(int value)
    {
        Buff buff = buffList.GetBuffName("취약");

        if (buff != null && buff.Value > 0) value += (int)(value * (Managers.Game.VulenrablePercent / 100f));
        buff = buffList.GetBuffName("공격모드");
        
        int temp = value;
        value -= Shield;
        Shield -= temp;
        if (value < 0) value = 0;
        if (Shield < 0)
        { 
            Shield = 0;
            if (buff != null)
            {
                buff.Value -= value;
                if (buff.Value <= 0)
                    RemoveBuff(buff);
            }

            if (!isAttackMode) { battleScene._playerController.Damaged(3); }
        }

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
    int AttackTurnTime = 0;
    int DefenseTurnTime = 0;
    public override void SetIntention()
    {
        Buff buff1 = buffList.GetBuffName("공격모드");
        Buff buff2 = buffList.GetBuffName("수비모드");
        if (buff1 == null && buff2 == null)
            GetDefenseMode(0);
        else if (buff2 != null && buff1 == null && !isAttackMode && DefenseTurnTime == 2) {
            RemoveBuff(buff2);
            GetAttackMode(40);
            DefenseTurnTime = 0;
            AttackTurnTime = 0;
        }
        if (isAttackMode)
        {
            switch (AttackTurnTime)
            {
                case 0:
                    curIntention = Intention.Defense;
                    IntentionFigure = 9;
                    AttackTurnTime++;
                    break;
                case 1:
                    curIntention = Intention.DeBuff;
                    IntentionFigure = 2;
                    AttackTurnTime++;
                    break;
                case 2:
                    curIntention = Intention.Attack;
                    IntentionFigure = 40;
                    AttackTurnTime++;
                    break;
                case 3:
                    curIntention = Intention.AttackMany;
                    IntentionFigure = 5;
                    AttackNum = 4;
                    AttackTurnTime = 0;
                    break;
            }
        }
        else if (!isAttackMode) {
            switch (DefenseTurnTime)
            {
                case 0:
                    curIntention = Intention.Attack;
                    IntentionFigure = 10;
                    DefenseTurnTime++;
                    break;
                case 1:
                    curIntention = Intention.AttackMany;
                    IntentionFigure = 9;
                    AttackNum = 2;
                    DefenseTurnTime++;
                    break;
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
            case Intention.DeBuff:
                battleScene._playerController.GetWeakness(2);
                battleScene._playerController.GetVulenrable(2);
                break;
            case Intention.Defense:
                GetShield(IntentionFigure);
                break;
            case Intention.AttackMany:
                for (int i = 0; i < AttackNum; i++) {
                    AttackPlayer(IntentionFigure);
                    yield return new WaitForSeconds(0.41f);
                }
                break;
        }
        yield return null;
        StartCoroutine(base.IntentionMotion());
    }
    public bool isAttackMode = true;
    public void GetAttackMode(int value) {
        Buff buff = buffList.GetBuffName("공격모드");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new AttackModeBuff { Name = "공격모드", Value = value, controller = this, des = Define.AttackMode });
        }
        isAttackMode = true;
        RefreshUI();
    }
    public void GetDefenseMode(int value)
    {
        Buff buff = buffList.GetBuffName("수비모드");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new DefenseModeBuff { Name = "수비모드", Value = value, controller = this, des = Define.DefenseMode });
        }
        isAttackMode = false;
        GetShield(20);
        RefreshUI();
    }
}
