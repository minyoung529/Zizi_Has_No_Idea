using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPosition : MonoBehaviour
{
    private Vector3 originPosition = Vector3.zero;
    private Quaternion originalRotation = Quaternion.identity;
    public bool isPlayer = false;
    public bool isConstantY = true;

    private void Awake()
    {
        originPosition = transform.position;
        
        if (isConstantY)
            originPosition.y = Constant.SPAWN_CHARACTER_Y;
        
        originalRotation = transform.rotation;

        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetObject);
    }

    private void ResetObject()
    {
        if (isPlayer)
        {
            transform.SetPositionAndRotation(GameManager.Instance.PlayerTransform.position, originalRotation);
        }
        else
        {
            transform.SetPositionAndRotation(originPosition, originalRotation);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetObject);
    }
}
