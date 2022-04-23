using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : UIButton
{
    protected override void OnClicked()
    {
        EventManager.TriggerEvent(Constant.START_PLAY_EVENT);
    }
}
