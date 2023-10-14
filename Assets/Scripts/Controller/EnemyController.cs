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
    public int UnitNumber = 0;
    public Intention curIntention = Intention.Nothing;
    public int IntentionFigure = 0;

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
    }
    RectTransform rect;
    enum GameObjects {
        CheckBody,
        ToolTip,
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
        GetObject((int)GameObjects.ToolTip).SetActive(false);
        rect = GetImage((int)Images.TooltipImage).rectTransform;

        RefreshUI();
        return true;
    }
    public void Damaged(int value)
    {
        if (Vulenerable > 0) value += (int)(value * (Managers.Game.VulenrablePercent/ 100f));
        int temp = value;
        value -= Shield;
        Shield -= temp;
        if (value < 0) value = 0;
        StartCoroutine(DamageMaterial());
        CurHp -= value;
        if (CurHp <= 0) { 
            CurHp = 0;
            // ����
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
        Damage += Power;
        if (Weakness > 0)
            Damage = (int)(Damage * 0.75f);
        battleScene._playerController.Damaged(Damage);
        //����
        animator.SetTrigger("Attack");
    }
    public void GetVulenrable(int value) {
        Vulenerable += value;
        if(!buffList.Contains("���"))
            buffList.Add("���");
        //����Ʈ �߰�
        RefreshUI();
    }   
    public void GetWeakness(int value) { 
        Weakness += value;
        if (!buffList.Contains("��ȭ"))
            buffList.Add("��ȭ");
        //����Ʈ �߰�
        RefreshUI();
    }
    public void GetShield(int value)
    {
        Shield = Shield + value + Agility;
        //����Ʈ �߰�
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
        Power += value;
        if (!buffList.Contains("��"))
            buffList.Add("��");
        RefreshUI();
    }
    public void GetdePower(int value)
    {
        dePower += value;
        if (!buffList.Contains("������"))
            buffList.Add("������");
        RefreshUI();
    }
    public void GetAgility(int value)
    {
        Agility += value;
        if (!buffList.Contains("��ø"))
            buffList.Add("��ø");
        RefreshUI();
    }
    public void GetPoisoning(int value)
    {
        Poisoning += value;
        if (!buffList.Contains("�ߵ�"))
            buffList.Add("�ߵ�");
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
            if (buff.gameObject.TryGetComponent(out UI_EventHandler ehd))
            {
                ehd.enabled = true;
            }
            if (buffList[i] == "���")
            {
                GetText(i + 1).text = Vulenerable.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Vulenerable); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "��ȭ")
            {
                GetText(i + 1).text = Weakness.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Weakness); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "��")
            {
                GetText(i + 1).text = Power.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Power); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "������")
            {
                GetText(i + 1).text = dePower.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.dePower); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "��ø")
            {
                GetText(i + 1).text = Agility.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Agility); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "�ߵ�")
            {
                GetText(i + 1).text = Poisoning.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Poisoning); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
        }
        for (int j = i; j < 7; j++)
        {
            var buff = GetImage(j + 1);
            buff.sprite = null;
            buff.color = new Color(1, 1, 1, 0);
            GetText(j + 1).text = "";
            if (buff.gameObject.TryGetComponent(out UI_EventHandler ehd)) {
                ehd.enabled = false;
            }
        }
        //�� üũ
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
        GameEvents.OnKillEnemy();
        Managers.Resource.Destroy(gameObject);
    }
    public void ResetBuff()
    {
        if (Vulenerable > 0) { Vulenerable--; buffList.Remove("���"); }
        if (Weakness > 0) { Weakness--; buffList.Remove("��ȭ"); }
        if (Shield > 0) { Shield = 0;}
        if (dePower > 0) { Power -= dePower; dePower = 0; buffList.Remove("������"); }
        if (Poisoning > 0) { Damaged(Poisoning); Poisoning--; buffList.Remove("�ߵ�"); }
        RefreshUI();
    }
    public void TooltipOn(Transform trf, int text)
    {
        var tooltip = GetObject((int)GameObjects.ToolTip);
        var txt = GetText((int)Texts.ToolTipText);
        txt.text = Managers.GetText(text);
        Vector3 pos = trf.position + new Vector3(1.5f,-1.5f,0);
        if (pos.x > 6.8f)
            pos = new Vector3(6.8f, pos.y, pos.z);
        tooltip.transform.position = pos;

        rect.sizeDelta = new Vector2(txt.preferredWidth + 20, txt.preferredHeight + 20);
        tooltip.SetActive(true);
    }
    public void TooltipOff()
    {
        GetObject((int)GameObjects.ToolTip).SetActive(false);
    }
    public virtual void SetIntention() { RefreshUIIntention(); }
    public virtual void RefreshUIIntention() {
        GetImage((int)Images.IntentionImage).color = new Color(1, 1, 1, 1);
        switch (curIntention) {
            case Intention.Attack:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/����");
                break;
            case Intention.AttackDefense:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/���ݹ��");
                break;
            case Intention.AttackDebuff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/���ݵ����");
                break;
            case Intention.Buff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/����");
                break;
            case Intention.DeBuff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/�����");
                break;
            case Intention.Defense:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/���");
                break;
            case Intention.DefenseBuff:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/������");
                break;
            case Intention.Nothing:
                GetImage((int)Images.IntentionImage).sprite = Managers.Resource.Load<Sprite>("Sprites/Icon/�ƹ��͵�����");
                break;
        }
        GetText((int)Texts.IntentionText).text = IntentionFigure.ToString();
    }
    public virtual void ResetIntention() {
        GetImage((int)Images.IntentionImage).color = new Color(1, 1, 1, 0);
        GetText((int)Texts.IntentionText).text = "";
        IntentionFigure = 0;
    }
    public virtual void IntentionMotion() { 
        ResetIntention();
    }
}
