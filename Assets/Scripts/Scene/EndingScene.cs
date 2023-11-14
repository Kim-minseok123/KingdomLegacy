using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        Managers.Sound.Play(Define.Sound.Bgm, "Bgm/Ending", Managers.Game.BgmSound);
        SceneType = Define.Scene.EndingScene;

        return true;
    }
    public void GoTitle() {
        TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/Fade/Fade"), 0, 
            () => { 
                Managers.Game.ClearGame();
                Managers.UI.CloseAllPopupUI();
                Managers.UI.ShowPopupUI<UI_TitlePopup>();
                Managers.Scene.ChangeScene(Define.Scene.GameScene);
            });
    }
}
