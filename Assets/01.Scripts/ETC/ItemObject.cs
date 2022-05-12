using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item item;
    public Item Item { get { return item; } }

    private void Awake()
    {
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, RegisterItem);
    }

    private void RegisterItem()
    {
        GameManager.Instance.CurrentItems.Add(this);
        Debug.Log($"{item.Name} µî·Ï");
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, RegisterItem);
    }
}