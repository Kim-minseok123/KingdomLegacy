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
        if(!(Name =="��ø" || Name == "��"))
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

