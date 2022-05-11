using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    private void Start()
    {
        Vector3 position = transform.position;
        position.x = Mathf.RoundToInt(position.x);
        position.z = Mathf.RoundToInt(position.z);

        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play)
        {
            Debug.Log("Get Star");
            EventManager.TriggerEvent(Constant.GET_STAR_EVENT);
        }
    }
}
