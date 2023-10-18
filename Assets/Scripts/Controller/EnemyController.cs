using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class EnemyController : UI_Base
{
    public int MaxHp = 0;
    public int CurHp = 0;
    public int Shield = 0;
    public int UnitNumber = 0;
    public Intention curIntention = Intention.Nothing;
    public int IntentionFigure = -1;

    public Animator animator;
    public UI_BattlePopup battleScene;
    public List<Buff> buffList = new();
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
        BuffImage8,
        TooltipImage,
        IntentionImage,
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
        BuffText8,
        ToolTipText,
        IntentionText,
        NameText,
    }
    RectTransform rect;
    enum GameObjects {
        CheckBody,
        ToolTip,
        NameText,
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
        GetText((int)Texts.NameText).DOFade(0, 1f);
        GetText((int)Texts.NameText).gameObject.SetActive(false);

        battleScene = Managers.UI.FindPopup<UI_BattlePopup>();
        animator = GetComponent<Animator>();

        Image uiImage = GetComponent<Image>();
        uiImage.material = new Material(uiImage.material);

        buffList.Clear();
        GetObject((int)GameObjects.ToolTip).SetActive(false);
        rect = GetImage((int)Images.TooltipImage).rectTransform;
        
        RefreshUI();
        return true;
    }
    public virtual void Damaged(int value)
    {
        Buff buff = buffList.GetBuffName("Ãë¾à");

        if (buff != null && buff.Value > 0) value += (int)(value * (Managers.Game.VulenrablePercent/ 100f));
        int temp = value;
        value -= Shield;
        Shield -= temp;
        if (value < 0) value = 0;
        if (Shield < 0) Shield = 0;
        StartCoroutine(DamageMaterial());
        CurHp -= value;
        if (CurHp <= 0) { 
            CurHp = 0;
            // Á×À½
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
            value += 0.04f;
            yield return null;
        }
        while (value > 0.01f)
        {
            material.SetFloat("_HitEffectBlend", value);
            value -= 0.04f;
            yield return null;
        }
        material.DisableKeyword("HITEFFECT_ON");
    }
    public void AttackPlayer(int Damage)
    {
        Buff buff = buffList.GetBuffName("Èû");
        if(buff != null)
            Damage += buffList.GetBuffName("Èû").Value;
        buff = buffList.GetBuffName("¾àÈ­");
        if (buff != null && buff.Value > 0)
            Damage = (int)(Damage * 0.75f);
        battleScene._playerController.Damaged(Damage);
        //°ø°Ý
        animator.SetTrigger("Attack");
    }
    public void RemoveBuff(Buff buff) { 
        buffList.Remove(buff);
    }
    
    public void GetVulenrable(int value) {
        Buff buff = buffList.GetBuffName("Ãë¾à");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new Buff { Name = "Ãë¾à", Value = value, controller = this, des = Define.Vulenerable});
        }
        else {
            buff.Value += value;
        }
        //ÀÌÆåÆ® Ãß°¡
        RefreshUI();
    }   
    public void GetWeakness(int value) {
        Buff buff = buffList.GetBuffName("¾àÈ­");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new Buff { Name = "¾àÈ­", Value = value, controller = this, des = Define.Weakness });
        }
        else
        {
            buff.Value += value;
        }
        //ÀÌÆåÆ® Ãß°¡
        RefreshUI();
    }
    public void GetShield(int value)
    {
        Buff buff = buffList.GetBuffName("¹ÎÃ¸");
        if(buff != null)
            Shield = Shield + value + Agility;
        else
            Shield = Shield + value;
        //ÀÌÆåÆ® Ãß°¡
        var effect = Managers.Resource.Instantiate("Effect/ShieldEffect", gameObject.transform);
        Managers.Resource.Destroy(effect, 0.45f);
        RefreshUI();
    }
    public void HalfShield() { 
        if(Shield > 0)
            Shield /= 2;
        RefreshUI();
    }
    public void GetPower(int value)
    {
        Buff buff = buffList.GetBuffName("Èû");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new Buff { Name = "Èû", Value = value, controller = this, des = Define.Power });
        }
        else
        {
            buff.Value += value;
        }
        RefreshUI();
    }
    public void GetdePower(int value)
    {
        Buff buff = buffList.GetBuffName("Èû°¨¼Ò");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new DePowerBuff { Name = "Èû°¨¼Ò", Value = value, controller = this , des = Define.dePower});
        }
        else
        {
            buff.Value += value;
        }
        RefreshUI();
    }
    public void GetAgility(int value)
    {
        Buff buff = buffList.GetBuffName("¹ÎÃ¸");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new Buff { Name = "¹ÎÃ¸", Value = value, controller = this , des = Define.Agility});
        }
        else
        {
            buff.Value += value;
        }
        RefreshUI();
    }
    public void GetPoisoning(int value)
    {
        Buff buff = buffList.GetBuffName("Áßµ¶");
        if (buff == null)
        {
            if (buffList.Count > 6)
                return;
            buffList.Add(new PoisonBuff { Name = "Áßµ¶", Value = value, controller = this , des = Define.Poisoning});
        }
        else
        {
            buff.Value += value;
        }
        RefreshUI();
    }
    public void RefreshUI()
    {
        int i = 0;
        
        for (i = 0; i < buffList.Count; i++)
        {
            var buffImage = GetImage(i + 1);
            var buff = buffList[i];
            buffImage.sprite = Managers.Resource.Load<Sprite>($"Sprites/Icon/{buff.Name}");
            buffImage.color = new Color(1, 1, 1, 1);
            if (buffImage.gameObject.TryGetComponent(out UI_EventHandler ehd))
            {
                ehd.enabled = true;
            }
            GetText(i + 1).text = buff.Value.ToString();
            buffImage.gameObject.BindEvent((go) => { TooltipOn(go.transform, buff.des); }, Define.UIEvent.PointerEnter);
            buffImage.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            
        }
        for (int j = i; j < 6; j++)
        {
            var buff = GetImage(j + 1);
            buff.sprite = null;
            buff.color = new Color(1, 1, 1, 0);
            GetText(j + 1).text = "";
            if (buff.gameObject.TryGetComponent(out UI_EventHandler ehd)) {
                ehd.enabled = false;
            }
        }
        //¹æ¾îµµ Ã¼Å©
        if (Shield > 0)
        {
            GetImage((int)Images.BuffImage8).color = new Color(1, 1, 1, 1);
            GetText((int)Texts.BuffText8).text = Shield.ToString();
        }
        else
        {
            GetImage((int)Images.BuffImage8).color = new Color(1, 1, 1, 0);
            GetText((int)Texts.BuffText8).text = "";
        }

        GetText((int)Texts.HpText).text = CurHp.ToString() + " / " + MaxHp.ToString();
        float value = CurHp / (float)MaxHp;
        GetImage((int)Images.HpValue).fillAmount = value;
    }
    public void Setting(int hp, int number) { 
        MaxHp = hp;
        CurHp = MaxHp;
        UnitNumber = number;
    }
    public void PointerEnterBody(GameObject go) {
        gameObject.GetComponent<Image>().material.EnableKeyword("OUTBASE_ON");
        battleScene._curEnemy = gameObject;
        GetText((int)Texts.NameText).gameObject.SetActive(true);
        GetText((int)Texts.NameText).DOFade(1, 0.5f);
    }
    public void PointerExitBody() {
        gameObject.GetComponent<Image>().material.DisableKeyword("OUTBASE_ON");
        battleScene._curEnemy = null;
        GetText((int)Texts.NameText).DOFade(0, 1f).OnComplete(() => { GetText((int)Texts.NameText).gameObject.SetActive(false); });
    }
    private void OnDestroy()
    {
        battleScene._curEnemy = null;
    }
    public void Death()
    {
        battleScene._enemyList.Remove(gameObject);
        GameEvents.OnKillEnemy();
        Managers.Resource.Destroy(gameObject);
    }
    public void ResetBuff()
    {
        for (int i = buffList.Count - 1; i > 0; i--) {
            buffList[i].Update();
        }
        if (Shield > 0) { Shield = 0;}

        RefreshUI();
    }
    public void TooltipOn(Transform trf, int text)
    {
        var tooltip = GetObject((int)GameObjects.ToolTip);
        var txt = GetText((int)Texts.ToolTipText);
        txt.text = Managers.GetText(text);
        Vector3 pos = trf.position + new Vector3(1.5f,-1.5f,0);
        if (pos.x > 5.5f)
            pos = new Vector3(5.5f, pos.y, pos.z);
        tooltip.transform.position = pos;

        rect.sizeDelta = new Vector2(txt.preferredWidth + 20, txt.preferredHeight + 20);
        tooltip.SetActive(true);
    }
    public void TooltipOff()
    {
        GetObject((int)GameObjects.ToolTip).SetActive(false);
    }
    public virtual void SetIntention() { RefreshUIIntention(); }
    public int AttackNum = 1;
    public virtual void RefreshUIIntention() {
        GetImage((int)Images.IntentionImage).color = new Color(1, 1, 1, 1);
        switch (curIntention) {
            case Intention.Attack: case Intention.AttackMany :
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/°ø°Ý");
                break;
            case Intention.AttackDefense:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/°ø°Ý¹æ¾î");
                break;
            case Intention.AttackDebuff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/°ø°Ýµð¹öÇÁ");
                break;
            case Intention.Buff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/¹öÇÁ");
                break;
            case Intention.DeBuff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/µð¹öÇÁ");
                break;
            case Intention.Defense:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/¹æ¾î");
                break;
            case Intention.DefenseBuff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/¹æ¾î¹öÇÁ");
                break;
            case Intention.Nothing:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/¾Æ¹«°Íµµ¾ÈÇÔ");
                break;
        }
        if (curIntention == Intention.AttackMany)
        {
            GetText((int)Texts.IntentionText).text = IntentionFigure.ToString() + " X " + AttackNum.ToString();
            return;
        }
        GetText((int)Texts.IntentionText).text = IntentionFigure.ToString();
    }
    public virtual void ResetIntention() {
        GetImage((int)Images.IntentionImage).color = new Color(1, 1, 1, 0);
        GetText((int)Texts.IntentionText).text = "";
        IntentionFigure = 0;
    }
    public virtual IEnumerator IntentionMotion() { 
        ResetIntention();
        yield return null;
    }
}
