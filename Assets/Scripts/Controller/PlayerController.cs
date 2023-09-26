
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
    }
    enum GameObjects
    {
        ToolTip,
    }
    RectTransform rect;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        _playerAnim = GetComponent<Animator>();

        buffList.Clear();
        GetObject((int)GameObjects.ToolTip).SetActive(false);
        rect = GetImage((int)Images.TooltipImage).rectTransform;
        RefreshUI();
        return true;
    }

    public void Damaged(int value)
    {
        //Ã¼·Â ´â±â
    }
    public void GetVulenrable(int value)
    {
        Vulenerable += value;
        if (!buffList.Contains("Ãë¾à"))
            buffList.Add("Ãë¾à");
        //ÀÌÆåÆ® Ãß°¡
        RefreshUI();
    }
    public void GetWeakness(int value)
    {
        Weakness += value;
        if (!buffList.Contains("¾àÈ­"))
            buffList.Add("¾àÈ­");
        //ÀÌÆåÆ® Ãß°¡
        RefreshUI();
    }
    public void GetShield(int value)
    {
        Shield = Shield + value + Agility;
        //ÀÌÆåÆ® Ãß°¡
        var effect = Managers.Resource.Instantiate("Effect/ShieldEffect", gameObject.transform);
        Managers.Resource.Destroy(effect, 0.45f);

        RefreshUI();
    }
    public void GetPower(int value)
    {
        Power = value;
        if (!buffList.Contains("Èû"))
            buffList.Add("Èû");
        RefreshUI();
    }
    public void GetdePower(int value)
    {
        dePower += value;
        if (!buffList.Contains("Èû°¨¼Ò"))
            buffList.Add("Èû°¨¼Ò");
        RefreshUI();
    }
    public void GetAgility(int value)
    {
        Agility += value;
        if (!buffList.Contains("¹ÎÃ¸"))
            buffList.Add("¹ÎÃ¸");
        RefreshUI();
    }
    public void GetPoisoning(int value)
    {
        Poisoning += value;
        if (!buffList.Contains("Áßµ¶"))
            buffList.Add("Áßµ¶");
        RefreshUI();
    }
    public void AttackEnemy(int Damage, EnemyController enemy = null)
    {
        if (Weakness > 0)
            Damage = (int)(Damage * 0.75f);
        //°ø°Ý
        if (enemy == null)
        {
            //ÀüÃ¼°ø°Ý
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
        for (int i = 0; i < 7; i++)
        {
            var buff = GetImage(i + 1);
            buff.sprite = null;
            buff.color = new Color(1, 1, 1, 0);
            GetText(i + 1).text = "";
            if(buff.gameObject.TryGetComponent<UI_EventHandler>(out var handler))
                Destroy(handler);
        }
        for (int i = 0; i < buffList.Count; i++)
        {
            var buff = GetImage(i + 1);
            buff.sprite = Managers.Resource.Load<Sprite>($"Sprites/Icon/{buffList[i]}");
            buff.color = new Color(1, 1, 1, 1);
            if (buffList[i] == "Ãë¾à")
            {
                GetText(i + 1).text = Vulenerable.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform,Define.Vulenerable); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "¾àÈ­")
            {
                GetText(i + 1).text = Weakness.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform,Define.Weakness); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "Èû")
            {
                GetText(i + 1).text = Power.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Power); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "Èû°¨¼Ò")
            {
                GetText(i + 1).text = dePower.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.dePower); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "¹ÎÃ¸")
            {
                GetText(i + 1).text = Agility.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Agility); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
            }
            else if (buffList[i] == "Áßµ¶")
            {
                GetText(i + 1).text = Poisoning.ToString();
                buff.gameObject.BindEvent((go) => { TooltipOn(go.transform, Define.Poisoning); }, Define.UIEvent.PointerEnter);
                buff.gameObject.BindEvent(() => { TooltipOff(); }, Define.UIEvent.PointerExit);
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
    public void TooltipOn(Transform trf ,int text) {

        var tooltip = GetObject((int)GameObjects.ToolTip);
        var txt = GetText((int)Texts.ToolTipText);
        txt.text = Managers.GetText(text);
        Vector3 pos = trf.position + new Vector3(0, -0.5f, 0);
        tooltip.transform.position = pos;
        rect.sizeDelta = new Vector2(txt.preferredWidth + 20, txt.preferredHeight + 20);
        tooltip.SetActive(true);
    }
    public void TooltipOff() { 
        GetObject((int)GameObjects.ToolTip).SetActive(false);
    }
}
