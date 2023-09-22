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
    public int Weakness = 0;
    public int Vulenerable = 0;
    public int Power = 0;
    public int Agility = 0;
    public int Poisoning = 0;
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

        Image uiImage = GetComponent<Image>();
        uiImage.material = new Material(uiImage.material);

        RefreshUI();
        return true;
    }
    public void Damaged(int value)
    {
        if (Vulenerable > 0) value += (int)(value * (Managers.Game.VulenrablePercent/ 100f));
        StartCoroutine(DamageMaterial());
        CurHp -= value;
        if (CurHp <= 0) { 
            CurHp = 0;
            // ¡◊¿Ω
            animator.SetTrigger("Death");
        }
        RefreshUI();
        
    }
    public IEnumerator DamageMaterial() {
        float value = 0f;
        Material material = GetComponent<Image>().material;
        //animator.SetTrigger("Stun");
        material.EnableKeyword("HITEFFECT_ON");
        while (value < 0.3f) {
            material.SetFloat("_HitEffectBlend", value);
            value += 0.02f;
            yield return null;
        }
        while (value > 0.01f)
        {
            material.SetFloat("_HitEffectBlend", value);
            value -= 0.02f;
            yield return null;
        }
        material.DisableKeyword("HITEFFECT_ON");
    }
    public void AttackPlayer()
    {
        //∞¯∞›
        animator.SetTrigger("Attack");
    }
    public void GetVulenrable(int value) {
        Vulenerable += value;
        //¿Ã∆Â∆Æ √ﬂ∞°
    }   
    public void GetWeakness(int value) { 
        Weakness += value;
        //¿Ã∆Â∆Æ √ﬂ∞°
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
    public void Death()
    {
        battleScene._enemyList.Remove(gameObject);
        Managers.Resource.Destroy(gameObject);
    }
}
