using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    private Renderer[] renderers;
    private bool isEnabled = true;

    private void Awake()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, EnabledRenderer);
    }

    public void EnabledRenderer()
    {
        if (isEnabled) return;

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        isEnabled = true;
        gameObject.SetActive(true);
    }

    public void DisabledRenderer()
    {
        renderers ??= gameObject.GetComponentsInChildren<Renderer>();
        
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        isEnabled = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, EnabledRenderer);
    }
}
