using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPosition : MonoBehaviour
{
    private Vector3 originPosition = Vector3.zero;
    private Quaternion originalRotation = Quaternion.identity;
    public bool isPlayer = false;
    public bool isConstantY = true;
    public bool isGrid = true;

    private void Awake()
    {
        Vector3 position = transform.position;
        if (isGrid)
        {
            position.x = Mathf.RoundToInt(position.x);
            position.z = Mathf.RoundToInt(position.z);
        }

        transform.position = position;
        originPosition = position;

        if (isConstantY)
            originPosition.y = Constant.SPAWN_CHARACTER_Y;

        originalRotation = transform.rotation;

        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetObject);
    }

    public void ResetObject()
    {
        if (isPlayer)
        {
            transform.SetPositionAndRotation(GameManager.Instance.PlayerTransform.position, GameManager.Instance.PlayerTransform.rotation);
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