using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticle : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening(Constant.GET_STAR_EVENT, ActiveParticle);
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, InactiveParticle);
        gameObject.SetActive(false);
    }

    private void ActiveParticle()
    {
        if(GameManager.Instance.StarCount == 0)
        {
            gameObject.SetActive(true);
        }
    }

    private void InactiveParticle()
    {
        gameObject.SetActive(false);
    }
}
