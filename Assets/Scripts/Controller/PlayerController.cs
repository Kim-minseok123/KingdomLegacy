
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
        //체력 닳기
    }
    public void GetVulenrable(int value)
    {
        Vulenerable += value;
        buffList.Add("취약");
        //이펙트 추가
        RefreshUI();
    }
    public void GetWeakness(int value)
    {
        Weakness += value;
        buffList.Add("약화");
        //이펙트 추가
        RefreshUI();
    }
    public void GetShield(int value)
    {
        Shield = Shield + value + Agility;
        //이펙트 추가
        RefreshUI();
    }
    public void GetPower(int value)
    {
        Power = value;
        RefreshUI();
    }
    public void GetdePower(int value)
    {
        dePower += value;
        RefreshUI();
    }
    public void GetAgility(int value) { 
        Agility += value;
        RefreshUI();
    }
    public void GetPoisoning(int value) { 
        Poisoning += value;
        RefreshUI();
    }
    public void AttackEnemy(int Damage, EnemyController enemy = null)
    {
        //공격
        if (enemy == null)
        {
            //전체공격
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
            if (buffList[i] == "취약")
                GetText(i + 1).text = Vulenerable.ToString();
            else if (buffList[i] == "약화")
                GetText(i + 1).text = Weakness.ToString();
            else if (buffList[i] == "힘")
                GetText(i + 1).text = Power.ToString();
            else if (buffList[i] == "힘감소")
                GetText(i + 1).text = dePower.ToString();
            else if (buffList[i] == "민첩")
                GetText(i + 1).text = Agility.ToString();
            else if (buffList[i] == "중독")
                GetText(i + 1).text = Poisoning.ToString();
        }

        for (int j = i; j < 7; j++)
        {
            var buff = GetImage(j + 1);
            buff.sprite = null;
            buff.color = new Color(1, 1, 1, 0);
            GetText(j + 1).text = "";
        }
        //방어도 체크
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
