
using UnityEngine;

public class PlayerController : UI_Base
{
    public int Weakness = 0;
    public int Vulenerable = 0;
    public int Power = 0;
    public int Agility = 0;
    public int Poisoning = 0;
    enum Images { 
        HpValue,
    }
    enum Texts { 
        HpText,
    }
    public Animator _playerAnim;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        _playerAnim = GetComponent<Animator>();

        RefreshUI();
        return true;
    }

    public void Damaged(int value) { 
        //체력 닳기
    }
    public void GetVulenrable(int value)
    {
        Vulenerable += value;
        //이펙트 추가
    }
    public void GetWeakness(int value)
    {
        Weakness += value;
        //이펙트 추가
    }
    public void AttackEnemy(int Damage, EnemyController enemy =null) {
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
        else if (enemy != null) { 
            enemy.Damaged(Damage);
        }
        _playerAnim.SetTrigger("Attack");
    }
    public void RefreshUI() { 
        GetText((int)Texts.HpText).text = Managers.Game.CurHp.ToString() + " / " + Managers.Game.MaxHp.ToString();
        float value = Managers.Game.CurHp / (float)Managers.Game.MaxHp;
        GetImage((int)Images.HpValue).fillAmount = value;
    }
    
}
