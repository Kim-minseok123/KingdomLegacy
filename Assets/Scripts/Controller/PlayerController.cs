
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UI_Base
{
    public int Weakness = 0;
    public int Vulenerable = 0;
    public int Power = 0;
    public int Agility = 0;
    public int Poisoning = 0;
    public int Shield = 0;
    public int dePower = 0;
    public Animator _playerAnim;

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
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        _playerAnim = GetComponent<Animator>();

        buffList.Clear();

        RefreshUI();
        return true;
    }

    public void Damaged(int value)
    {
        //√º∑¬ ¥‚±‚
    }
    public void GetVulenrable(int value)
    {
        Vulenerable += value;
        if (!buffList.Contains("√Îæ‡"))
            buffList.Add("√Îæ‡");
        //¿Ã∆Â∆Æ √ﬂ∞°
        RefreshUI();
    }
    public void GetWeakness(int value)
    {
        Weakness += value;
        if (!buffList.Contains("æ‡»≠"))
            buffList.Add("æ‡»≠");
        //¿Ã∆Â∆Æ √ﬂ∞°
        RefreshUI();
    }
    public void GetShield(int value)
    {
        Shield = Shield + value + Agility;
        //¿Ã∆Â∆Æ √ﬂ∞°
        var effect = Managers.Resource.Instantiate("Effect/ShieldEffect", gameObject.transform);
        Managers.Resource.Destroy(effect, 0.45f);

        RefreshUI();
    }
    public void GetPower(int value)
    {
        Power = value;
        if (!buffList.Contains("»˚"))
            buffList.Add("»˚");
        RefreshUI();
    }
    public void GetdePower(int value)
    {
        dePower += value;
        if (!buffList.Contains("»˚∞®º“"))
            buffList.Add("»˚∞®º“");
        RefreshUI();
    }
    public void GetAgility(int value)
    {
        Agility += value;
        if (!buffList.Contains("πŒ√∏"))
            buffList.Add("πŒ√∏");
        RefreshUI();
    }
    public void GetPoisoning(int value)
    {
        Poisoning += value;
        if (!buffList.Contains("¡ﬂµ∂"))
            buffList.Add("¡ﬂµ∂");
        RefreshUI();
    }
    public void AttackEnemy(int Damage, EnemyController enemy = null)
    {
        //∞¯∞›
        if (enemy == null)
        {
            //¿¸√º∞¯∞›
            var Enemys = Managers.UI.FindPopup<UI_BattlePopup>()._enemyList;
            foreach (GameObject go in Enemys)
            {
                go.GetComponent<EnemyController>().Damaged(Damage);
            }
        }
        else if (enemy != null)
        {
            enemy.Damaged(Damage);
        }
        _playerAnim.SetTrigger("Attack");
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
        //πÊæÓµµ √º≈©
        if (Shield > 0) {
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
    public void ResetBuff()
    {
        if (Vulenerable > 0) { Vulenerable--; }
        if (Weakness > 0) { Weakness--; }
        if (Shield > 0) { Shield = 0; }
        if (dePower > 0) { Power -= dePower; dePower = 0; }
        if (Poisoning > 0) { Damaged(Poisoning); Poisoning--; }
    }
}
