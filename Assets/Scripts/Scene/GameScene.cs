
using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
		if (base.Init() == false)
			return false;

        SceneType = Define.Scene.Game;

		TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/Fade/Fade"), 0,() => { Managers.UI.ShowPopupUI<UI_TitlePopup>(); });

		Debug.Log("Init");
		return true;
	}
}
