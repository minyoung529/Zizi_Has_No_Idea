using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    private void Awake()
    {
        RegisterStarCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play)
        {
            Debug.Log("Get Star");
            if (--GameManager.Instance.StarCount == 0)
            {
                EventManager.TriggerEvent(Constant.GET_STAR_EVENT);
            }
        }
    }

    private void RegisterStarCount()
    {
        ++GameManager.Instance.StarCount;
    }
}
