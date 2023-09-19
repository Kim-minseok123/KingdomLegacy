
using UnityEngine;

public class PlayerController : UI_Base
{
    enum Images { 
        HpValue,
    }
    enum Texts { 
        HpText,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        RefreshUI();
        return true;
    }

    public void Damaged() { 
        //체력 닳기
    }
    public void AttackEnemy() { 
        //공격
    }
    public void RefreshUI() { 
        GetText((int)Texts.HpText).text = Managers.Game.CurHp.ToString() + " / " + Managers.Game.MaxHp.ToString();
        float value = Managers.Game.CurHp / (float)Managers.Game.MaxHp;
        GetImage((int)Images.HpValue).fillAmount = value;
    }
}
