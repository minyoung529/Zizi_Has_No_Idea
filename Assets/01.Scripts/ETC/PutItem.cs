using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutItem : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private void Awake()
    {
        if (item.GetComponent<StarObject>())
        {
            EventManager.StartListening(Constant.RESET_GAME_EVENT, RegisterStarCount);
        }
    }

    public void PutItemOnCurrentPos()
    {
        GameObject obj = PoolManager.Pop(item);
        obj.transform.position = transform.position;
        obj.transform.SetParent(transform.parent);

        StarObject star = obj.GetComponent<StarObject>();

        if (star)
        {
            star.SkipRegister = true;
        }
    }

    private void RegisterStarCount()
    {
        GameManager.Instance.StarCount += 1;
    }
}