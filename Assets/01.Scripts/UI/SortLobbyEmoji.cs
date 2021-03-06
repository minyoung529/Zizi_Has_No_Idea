using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SortLobbyEmoji : MonoBehaviour
{
    private ChangeUISprite spriteChanger;

    private const string START = "Play";
    private const string STOP = "Stop";
    private const string SETTING = "Setting";

    [SerializeField] private UnityEvent onChange;
    [SerializeField] private UnityEvent onFailToChange;

    private void Start()
    {
        spriteChanger = GetComponent<ChangeUISprite>();
    }

    public void OnSelected()
    {
        if (spriteChanger.IsInPointer())
        {
            switch (DraggingUI.currentDraggedUI.gameObject.name)
            {
                case START:
                    EventManager.TriggerEvent(Constant.GAME_START_EVENT);
                    break;
                case STOP:
                    Application.Quit();
                    break;
                case SETTING:
                    GameManager.Instance.UIManager.ActiveSetting();
                    break;
            }

            onChange.Invoke();
            spriteChanger.ResetData();
        }
        else
        {
            onFailToChange.Invoke();
        }
    }
}
