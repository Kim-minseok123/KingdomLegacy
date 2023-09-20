using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using System;
using UnityEngine.UI;

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
    enum GameObjects { 
        CheckBody,
    }
    UI_BattlePopup battleScene;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        GetObject((int)GameObjects.CheckBody).BindEvent((go) => PointerEnterBody(go), UIEvent.PointerEnter);
        GetObject((int)GameObjects.CheckBody).BindEvent(PointerExitBody, UIEvent.PointerExit);

        battleScene = Managers.UI.FindPopup<UI_BattlePopup>();
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
    public void PointerEnterBody(GameObject go) {
        gameObject.GetComponent<Image>().material.EnableKeyword("OUTBASE_ON");
        battleScene._curEnemy = gameObject;
    }
    public void PointerExitBody() {
        gameObject.GetComponent<Image>().material.DisableKeyword("OUTBASE_ON");
        battleScene._curEnemy = null;
    }
    private void OnDestroy()
    {
        battleScene._curEnemy = null;
    }
}
