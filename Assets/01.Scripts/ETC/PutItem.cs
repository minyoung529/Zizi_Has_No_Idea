using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutItem : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private GenerateObject generateObject;

    private void Awake()
    {
        generateObject = item.GetComponent<GenerateObject>();

        if (generateObject)
        {
            EventManager.StartListening(Constant.RESET_GAME_EVENT, RegisterStarCount);
        }
    }

    public void PutItemOnCurrentPos()
    {
        GameObject obj = PoolManager.Pop(item);
        obj.transform.position = transform.position;
        obj.transform.SetParent(transform.parent);
    }

    private void RegisterStarCount()
    {
        GameManager.Instance.StarCount += 1;
    }
}