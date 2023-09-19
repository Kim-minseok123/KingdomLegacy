using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using System;

public class EnemyController : UI_Base
{
	public int MaxHp = 0;
    public int CurHp = 0;
    public Animator animator;
    enum Images
    {
        HpValue,
    }
    enum Texts
    {
        HpText,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        animator = GetComponent<Animator>();

        RefreshUI();
        return true;
    }
    public void Damaged()
    {
        //체력 닳기
        animator.SetTrigger("Stun");
    }
    public void AttackPlayer()
    {
        //공격
        animator.SetTrigger("Attack");
    }
    public void RefreshUI()
    {
        GetText((int)Texts.HpText).text = CurHp.ToString() + " / " + MaxHp.ToString();
        float value = CurHp / (float)MaxHp;
        GetImage((int)Images.HpValue).fillAmount = value;
    }
    public void Setting(int hp) { 
        MaxHp = hp;
        CurHp = MaxHp;
    }
}
