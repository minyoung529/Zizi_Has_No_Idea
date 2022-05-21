using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLobbyEmoji : MonoBehaviour
{
    private ChangeUISprite spriteChanger;

    private const string START = "Play";
    private const string STOP = "Stop";
    private const string SETTING = "Setting";

    private void Start()
    {
        spriteChanger = GetComponent<ChangeUISprite>();
    }

    public void OnSelected()
    {
        if(spriteChanger.IsInPointer())
        {
            switch(DraggingUI.currentDraggedUI.gameObject.name)
            {
                case START:
                    Debug.Log("sdf");
                    EventManager.TriggerEvent(Constant.GAME_START_EVENT);
                    break;
                case STOP:
                    Application.Quit();
                    break;
                case SETTING:
                    // TODO: 설정창 만들기
                    break;
            }
        }
    }
}
