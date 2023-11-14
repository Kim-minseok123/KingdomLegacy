using Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_ChooseClearItemPopup : UI_Popup
{
    enum Transforms
    {
        
    }
    enum Buttons
    {
        EndButton,
    }

    public override bool Init()
    {
        if (!base.Init())
        {
            return false;
        }

        BindButton(typeof(Buttons));
        Bind<Transform>(typeof(Transforms));
        if (Managers.Game.CurMapNode.Node.nodeType == NodeType.Boss) {
            BossItem();
        }
        else
            SetItem();

        GetButton((int)Buttons.EndButton).gameObject.BindEvent(EndSelect);
        return true;
    }
    public void EndSelect()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        GetComponent<Animator>().SetTrigger("Off");
    }
    void OnComplete()
    {
        if (Managers.UI.FindPopup<UI_BattlePopup>() == null)
        {
            Managers.UI.FindPopup<UI_MapPopup>().SideBarOn();
        }
        Managers.UI.ClosePopupUI(this);
    }
    public void SetItem() {
        int type;
        if (Managers.UI.FindPopup<UI_BattlePopup>() != null)
        {
            type = 2;
            
        }
        else {
            type = 3;
        }

        List<int> ItemList = new();
        for (int i = 10; i <= 67; i++)
        {
            ItemList.Add(i);
        }
        ItemList.Remove(64);
        for (int i = 0; i < Managers.Game.Items.Count; i++) {
            if (ItemList.Contains(Managers.Game.Items[i]))
                ItemList.Remove(Managers.Game.Items[i]);
        }
        if (type == 2)
        {
            if (Managers.Game.isChoice)
            {
                int random = ItemList.Random();

                var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                obj.transform.localPosition = new Vector3(200f, 200f, 0f);
                obj.GetComponent<UI_Item>().SetInfo(random, type);
                random = ItemList.Random();

                obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                obj.transform.localPosition = new Vector3(-200f, 200f, 0f);
                obj.GetComponent<UI_Item>().SetInfo(random, type);
            }
            else
            {
                int random = ItemList.Random();

                var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                obj.transform.localPosition = new Vector3(0f, 200f, 0f);
                obj.GetComponent<UI_Item>().SetInfo(random, type);
            }
        }
        else if (type == 3) {
            if (Managers.Game.isDoubleItem) {
                int random = ItemList.Random();

                var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                obj.transform.localPosition = new Vector3(200f, 200f, 0f);
                obj.GetComponent<UI_Item>().SetInfo(random, type);
                random = ItemList.Random();

                obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                obj.transform.localPosition = new Vector3(-200f, 200f, 0f);
                obj.GetComponent<UI_Item>().SetInfo(random, type);
            }
            else
            {
                int random = ItemList.Random();

                var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                obj.transform.localPosition = new Vector3(0f, 200f, 0f);
                obj.GetComponent<UI_Item>().SetInfo(random, type);
            }
        }
    }
    public void BossItem()
    {
        float x = -300f;
        switch (Managers.Game.Stage) {
            case 1:
                for (int i = 4; i < 7; i++)
                {
                    var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                    obj.transform.localPosition = new Vector3(x, 200f, 0f);
                    obj.GetComponent<UI_Item>().SetInfo(i, 2);
                    x += 300f;
                }
                break;
            case 2:
                for (int i = 7; i < 10; i++)
                {
                    var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
                    obj.transform.localPosition = new Vector3(x, 200f, 0f);
                    obj.GetComponent<UI_Item>().SetInfo(i, 2);
                    x += 300f;
                }
                break;
        }
    }
}
