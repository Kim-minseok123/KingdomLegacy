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

        SetItem();

        GetButton((int)Buttons.EndButton).gameObject.BindEvent(EndSelect);
        return true;
    }
    public void EndSelect()
    {
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
        int random;

        if (Managers.Game.isChoice || Managers.Game.isDoubleItem)
        {
            do
            {
                random = Random.Range(10, 68);
            } while (random == 64);

            var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
            obj.transform.localPosition = new Vector3(200f, 200f, 0f);
            obj.GetComponent<UI_Item>().SetInfo(random, type);

            do
            {
                random = Random.Range(10, 68);
            } while (random == 64);
            obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
            obj.transform.localPosition = new Vector3(-200f, 200f, 0f);
            obj.GetComponent<UI_Item>().SetInfo(random, type);
        }
        else
        {
            do
            {
                random = Random.Range(10, 68);
            } while (random == 64);

            var obj = Managers.Resource.Instantiate("UI/SubItem/UI_Item", transform);
            obj.transform.localPosition = new Vector3(0f, 200f, 0f);
            obj.GetComponent<UI_Item>().SetInfo(random, type);
        }
    }
}
