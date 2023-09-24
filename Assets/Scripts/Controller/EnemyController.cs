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
    public int Shield = 0;
    public int dePower = 0;

    public Animator animator;
    UI_BattlePopup battleScene;
    public List<string> buffList = new();
    enum Images
    {
        HpValue,
        BuffImage1,
        BuffImage2,
        BuffImage3,
        BuffImage4,
        BuffImage5,
        BuffImage6,
        BuffImage7,
    }
    enum Texts
    {
        HpText,
        BuffText1,
        BuffText2,
        BuffText3,
        BuffText4,
        BuffText5,
        BuffText6,
        BuffText7,
    }
    enum GameObjects {
        CheckBody,
    }
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

        buffList.Clear();

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
        buffList.Add("√Îæ‡");
        //¿Ã∆Â∆Æ √ﬂ∞°
        RefreshUI();
    }   
    public void GetWeakness(int value) { 
        Weakness += value;
        buffList.Add("æ‡»≠");
        //¿Ã∆Â∆Æ √ﬂ∞°
        RefreshUI();
    }
    public void RefreshUI()
    {
        int i = 0;
        for (i = 0; i < buffList.Count; i++)
        {
            var buff = GetImage(i + 1);
            buff.sprite = Managers.Resource.Load<Sprite>($"Sprites/Icon/{buffList[i]}");
            buff.color = new Color(1, 1, 1, 1);
            if (buffList[i] == "√Îæ‡")
                GetText(i + 1).text = Vulenerable.ToString();
            else if (buffList[i] == "æ‡»≠")
                GetText(i + 1).text = Weakness.ToString();
            else if (buffList[i] == "»˚")
                GetText(i + 1).text = Power.ToString();
            else if (buffList[i] == "»˚∞®º“")
                GetText(i + 1).text = dePower.ToString();
            else if (buffList[i] == "πŒ√∏")
                GetText(i + 1).text = Agility.ToString();
            else if (buffList[i] == "¡ﬂµ∂")
                GetText(i + 1).text = Poisoning.ToString();
        }

        for (int j = i; j < 7; j++)
        {
            var buff = GetImage(j + 1);
            buff.sprite = null;
            buff.color = new Color(1, 1, 1, 0);
            GetText(j + 1).text = "";
        }
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
