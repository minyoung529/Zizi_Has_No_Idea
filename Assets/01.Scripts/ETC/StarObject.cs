using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play)
        {
            Debug.Log("Get Star");
            EventManager.TriggerEvent(Constant.GET_STAR_EVENT);
        }
    }
}
