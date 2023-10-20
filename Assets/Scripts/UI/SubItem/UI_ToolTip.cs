using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_ToolTip : UI_Base
{
    string des;
    Vector3 parentPos;
    enum Texts { 
        ToolTipText,
    }
    public override bool Init()
    {
        if (!base.Init())
            return false;
        BindText(typeof(Texts));
        
        if (transform.parent.transform.localPosition.x > 0) {
            transform.localPosition = new Vector3(-pos.x, pos.y, pos.z);
        }
        else
        {
            transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        }
        RefreshUI();
        return true;
    }
    public Vector3 pos;
    public RectTransform rt;
    private void RefreshUI()
    {
        GetText((int)Texts.ToolTipText).text = des;
    }
    public void SetInfo(int index, Vector3 pos) {
        des = Managers.GetText(index);
        this.pos = pos;
    }
}
