using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChooseClearCardPopup : UI_Popup
{
    enum Transforms { 
        CardTransform1,
        CardTransform2,
        CardTransform3,
    }
    enum Buttons { 
        EndButton,
    }

    public override bool Init()
    {
        if (!base.Init()) { 
            return false;
        }

        BindButton(typeof(Buttons));
        Bind<Transform>(typeof(Transforms));


        return true;
    }
}
