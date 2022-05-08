using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item item;
    public Item Item { get { return item; } }

    private void Start()
    {
        Vector3 position = transform.position;
        position.x = Mathf.RoundToInt(position.x);
        position.z = Mathf.RoundToInt(position.z);

        transform.position = position;
    }

    private void Update()
    {
        item.ItemPosition = transform.position;
    }
}