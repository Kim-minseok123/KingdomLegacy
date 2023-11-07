using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    public string Name;
    public int Value;
    public int des;
    public EnemyController controller;
    public virtual void Update() {
        if(!(Name =="¹ÎÃ¸" || Name == "Èû"))
            Value--;
        if (Value == 0) {
            controller.RemoveBuff(this); 
        }
    }
}
public class DePowerBuff : Buff {
    public override void Update()
    {
        controller.GetPower(-Value);
        Value = 0;
        controller.RemoveBuff(this);
    }
}
public class PoisonBuff : Buff {
    public override void Update()
    {
        controller.Damaged(Value);
        base.Update();
    }
}
public class BarrierBuff : Buff
{
    public override void Update()
    {
        return;
    }
}
public class ShoutBuff : Buff
{
    public override void Update()
    {
        return;
    }
}
public class AttackModeBuff : Buff {
    public override void Update()
    {
        return;
    }
}
public class DefenseModeBuff : Buff {
    public override void Update()
    {
        return;
    }
}
public class ArmorBuff : Buff {
    public override void Update()
    {
        controller.GetShield(Value);
    }
}
public class LightShield : Buff {
    public override void Update()
    {
        
    }
}
