
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class PlayerController : UI_Base
{
    public int Weakness = 0;
    public int Vulenerable = 0;
    public int Power = 0;
    public int Agility = 0;
    public int Poisoning = 0;
    public int Shield = 0;
    public int dePower = 0;
    public int infinitySword = 0;
    public bool isInfinitySword = false;
    public Animator _playerAnim;
    public int Inviolable = 0;
    public int Confusion = 0;
    public int Restraint = 0;
    public int BeDamaged = 0;
    public int Gag = 0;
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
        NameText,
    }
    enum GameObjects
    {
        ToolTip,
        CheckBody,
        Posion
    }
    RectTransform rect;
    public UI_BattlePopup _battleScene;
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

        _playerAnim = GetComponent<Animator>();

        buffList.Clear();
        GetObject((int)GameObjects.ToolTip).SetActive(false);
        rect = GetImage((int)Images.TooltipImage).rectTransform;
        _battleScene = Managers.UI.FindPopup<UI_BattlePopup>();
        RefreshUI();
        return true;
    }
    public void PointerEnterBody(GameObject go)
    {
        GetText((int)Texts.NameText).DOFade(1, 0.5f);
    }
    public void PointerExitBody()
    {
        GetText((int)Texts.NameText).DOFade(0, 1f);
    }

    public void Damaged(int value, string enemy = null)
    {
        //체력 닳기
        if (Vulenerable > 0) value += (int)(value * (50 / 100f));
        int temp = value;
        value -= Shield;
        Shield -= temp;
        if(value < 0) value = 0;
        if (Shield < 0) Shield = 0;
        StartCoroutine(DamageMaterial());
        if (value > 0 && Inviolable > 0)
        {
            value = 1;
            ResetInviolable();
        }
        var effect = Managers.Resource.Instantiate("Effect/Hit");
        effect.transform.position = transform.position;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/피격", Managers.Game.EffectSound);
        Managers.Game.CurHp -= value;
        if(value > 0)
            GameEvents.OnLostHp();
        if (Managers.Game.CurHp <= 0 && !Managers.Game.isResurrection)
        {
            Managers.Game.CurHp = 0;
            _playerAnim.SetTrigger("Death");
            Managers.UI.ShowPopupUI<UI_DeathPopup>().SetInfo(enemy);
        }
        else if (Managers.Game.CurHp <= 0 && Managers.Game.isResurrection) { 
            Managers.Game.CurHp = 1;
            Managers.Game.isResurrection = false;
        }
        RefreshUI();
    }
    public IEnumerator DamageMaterial()
    {
        float value = 0f;
        Material material = GetComponent<Image>().material;
        //animator.SetTrigger("Stun");
        material.EnableKeyword("HITEFFECT_ON");
        while (value < 0.3f)
        {
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
    public void GetVulenrable(int value)
    {
        if (Managers.Game.isNonDebuff)
            return;
        Vulenerable += value;
        if (!buffList.Contains("취약"))
            buffList.Add("취약");
        //이펙트 추가
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void GetWeakness(int value)
    {
        if (Managers.Game.isNonDebuff)
            return;
        Weakness += value;
        if (!buffList.Contains("약화"))
            buffList.Add("약화");
        //이펙트 추가
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void GetShield(int value)
    {
        Shield = Shield + value + Agility;
        if (BeDamaged > 0) {
            Shield = (int)(Shield * 0.7f);
           
        }
        if (Shield < 0) Shield = 0;
        //이펙트 추가
        var obj = transform.Find("ShieldEffect");
        if (obj != null)
        {
            RefreshUI();
            return;
        }
        var effect = Managers.Resource.Instantiate("Effect/ShieldEffect", gameObject.transform);
        Managers.Resource.Destroy(effect, 0.45f);
        Managers.Sound.Play(Define.Sound.Effect, "Effect/방어", Managers.Game.EffectSound);
        RefreshUI();
    }
    public void GetPower(int value)
    {
        Power += value;
        if (!buffList.Contains("힘"))
            buffList.Add("힘");
        var effect = Managers.Resource.Instantiate("Effect/Buff");
        effect.transform.position = transform.position;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);
        RefreshUI();
    }
    public void GetdePower(int value)
    {
        dePower += value;
        if (!buffList.Contains("힘감소"))
            buffList.Add("힘감소");
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void GetAgility(int value)
    {
        Agility += value;
        if (!buffList.Contains("민첩"))
            buffList.Add("민첩");
        var effect = Managers.Resource.Instantiate("Effect/Buff");
        effect.transform.position = transform.position;
        if(value > 0)
            Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);
        RefreshUI();
    }
    public void GetPoisoning(int value)
    {
        Poisoning += value;
        if (!buffList.Contains("중독"))
            buffList.Add("중독");
        var effect = Managers.Resource.Instantiate("Effect/Poison");
        effect.transform.position = GetObject((int)GameObjects.Posion).transform.position;
        RefreshUI();
    }
    public void GetSwordGauge(int value) {
        infinitySword += value;
        if (!buffList.Contains("무한의검"))
            buffList.Add("무한의검");
        var effect = Managers.Resource.Instantiate("Effect/Buff");
        effect.transform.position = transform.position;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);
        RefreshUI();

    }
    public void ResetSwordGauge() { 
        infinitySword = 0;
        if (buffList.Contains("무한의검"))
            buffList.Remove("무한의검");
        RefreshUI();

    }
    public void GetInviolable(int value) {
        Inviolable = value;
        if (!buffList.Contains("불가침"))
            buffList.Add("불가침");
        var effect = Managers.Resource.Instantiate("Effect/Buff");
        effect.transform.position = transform.position;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/버프", Managers.Game.EffectSound);
        RefreshUI();
    }
    public void GetBeDamaged(int value)
    {
        BeDamaged = value;
        if (!buffList.Contains("손상"))
            buffList.Add("손상");
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void GetConfusion(int value)
    {
        Confusion = value;
        if (!buffList.Contains("혼란"))
            buffList.Add("혼란");
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void GetRestraint(int value)
    {
        Restraint = value;
        if (!buffList.Contains("속박"))
            buffList.Add("속박");
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void GetGag(int value)
    {
        Gag = value;
        if (!buffList.Contains("재갈"))
            buffList.Add("재갈");
        var effect = Managers.Resource.Instantiate("Effect/DeBuff");
        effect.transform.position = transform.position;
        RefreshUI();
    }
    public void ResetInviolable()
    {
        Inviolable = 0;
        if (buffList.Contains("불가침"))
            buffList.Remove("불가침");
        RefreshUI();

    }
    public void AttackEnemy(int Damage, EnemyController enemy = null)
    {
        if (Weakness > 0)
            Damage = (int)(Damage * 0.75f);
        //공격
        if (enemy == null)
        {
            //전체공격
            var Enemys = Managers.UI.FindPopup<UI_BattlePopup>()._enemyList;
            for (int i = Enemys.Count - 1; i >= 0; i--)
            {
                Enemys[i].GetComponent<EnemyController>().Damaged(Damage);
            }
        }
        else if (enemy != null)
        {
            enemy.Damaged(Damage);
        }
        _playerAnim.SetTrigger("Attack");
        Managers.Sound.Play(Define.Sound.Effect, "Effect/공격", Managers.Game.EffectSound);
    }
    public void AttackEnemy(int Damage, bool Heal, EnemyController enemy = null)
    {
        if (Weakness > 0)
            Damage = (int)(Damage * 0.75f);
        //공격
        if (enemy == null)
        {
            //전체공격
            var Enemys = Managers.UI.FindPopup<UI_BattlePopup>()._enemyList;
            for (int i = Enemys.Count - 1; i >= 0; i--)
            {
                Enemys[i].GetComponent<EnemyController>().Damaged(Damage);
            }
        }
        else if (enemy != null)
        {
            enemy.Damaged(Damage);
        }
        HealHp(Damage);
        _playerAnim.SetTrigger("Attack");
        Managers.Sound.Play(Define.Sound.Effect, "Effect/공격", Managers.Game.EffectSound);
    }
    public void RefreshUI()
    {
        int i = 0;
        int count = buffList.Count;
        if(count > 6) count = 6;
        for (i = 0; i < count; i++)
        {
            var buff = GetImage(i + 1);
            buff.sprite = Managers.Resource.Load<Sprite>($"Sprites/Icon/{buffList[i]}");
            buff.color = new Color(1, 1, 1, 1);
            if (buff.gameObject.TryGetComponent(out UI_EventHandler ehd))
            {
                ehd.enabled = true;
            }
            if (buffList[i] == "취약")
            {
                GetText(i + 1).text = Vulenerable.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Vulenerable); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "약화")
            {
                GetText(i + 1).text = Weakness.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Weakness); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "힘")
            {
                GetText(i + 1).text = Power.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Power); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "힘감소")
            {
                GetText(i + 1).text = dePower.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.dePower); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "민첩")
            {
                GetText(i + 1).text = Agility.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Agility); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "중독")
            {
                GetText(i + 1).text = Poisoning.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Poisoning); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "무한의검")
            {
                GetText(i + 1).text = infinitySword.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.infinitySword); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "불가침")
            {
                GetText(i + 1).text = Inviolable.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Inviolable); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "혼란")
            {
                GetText(i + 1).text = "";
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Confusion); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "속박")
            {
                GetText(i + 1).text = "";
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Restraint); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "손상")
            {
                GetText(i + 1).text = BeDamaged.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.BeDamaged); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "재갈")
            {
                GetText(i + 1).text = Gag.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Gag); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
        }
        for (int j = i; j < 7; j++)
        {
            var buff = GetImage(j + 1);
            buff.sprite = null;
            buff.color = new Color(1, 1, 1, 0);
            GetText(j + 1).text = "";
            if (buff.gameObject.TryGetComponent(out UI_EventHandler ehd))
            {
                ehd.enabled = false;
            }
        }
        //방어도 체크
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

        GetText((int)Texts.HpText).text = Managers.Game.CurHp.ToString() + " / " + Managers.Game.MaxHp.ToString();
        float value = Managers.Game.CurHp / (float)Managers.Game.MaxHp;
        GetImage((int)Images.HpValue).fillAmount = value;
    }
    public bool isDisappearShield = false;
    public void ResetBuff()
    {
        if (Vulenerable > 0) { Vulenerable--; if(Vulenerable == 0) buffList.Remove("취약"); }
        if (BeDamaged > 0) { BeDamaged--; if (BeDamaged == 0) buffList.Remove("손상"); }
        if (Gag > 0) { Gag--; if (Gag == 0) buffList.Remove("재갈"); }
        if (Weakness > 0) { Weakness--; if (Weakness == 0) buffList.Remove("약화"); }
        if (dePower > 0) {GetPower(-dePower); dePower = 0; buffList.Remove("힘감소"); }
        if (Power == 0) { buffList.Remove("힘"); }
        if (Poisoning > 0) { Poisoning--; if (Poisoning == 0) buffList.Remove("중독"); }
        RefreshUI();
    }
    public void TooltipOn(Transform trf ,int text) {

        var tooltip = GetObject((int)GameObjects.ToolTip);
        var txt = GetText((int)Texts.ToolTipText);
        txt.text = Managers.GetText(text);
        Vector3 pos = trf.position + new Vector3(-1.5f, -1.5f, 0);
        tooltip.transform.position = pos;
        rect.sizeDelta = new Vector2(txt.preferredWidth + 20, txt.preferredHeight + 20);
        tooltip.SetActive(true);
    }
    public void TooltipOff() { 
        GetObject((int)GameObjects.ToolTip).SetActive(false);
    }
    public void HealHp(int value) {
        Managers.Game.CurHp += value;
        if(Managers.Game.CurHp > Managers.Game.MaxHp)
            Managers.Game.CurHp = Managers.Game.MaxHp;
        RefreshUI();
    }
    public void ResetShield()
    {
        if (!isDisappearShield) if (Shield > 0) { Shield = 0; }
        if (Poisoning > 0)
        {
            Damaged(Poisoning);
        }
        RefreshUI();
    }
}
