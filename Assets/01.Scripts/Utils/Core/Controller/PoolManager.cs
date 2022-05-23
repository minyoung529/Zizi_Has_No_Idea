using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();
    private static Transform poolTransform;

    private void Awake()
    {
        poolTransform = transform;
    }

    public static void Push(GameObject obj)
    {
        obj.name = obj.name.Trim();
        obj.SetActive(false);
        obj.transform.position = Vector3.one;

        if (!pools.ContainsKey(obj.name))
        {
            pools.Add(obj.name, new Stack<GameObject>());
        }

        obj.transform.SetParent(poolTransform);
        pools[obj.name].Push(obj);
    }

    public static GameObject Pop(GameObject item)
    {
        item.name = item.name.Trim();
        GameObject value = null;

        if (pools.ContainsKey(item.name))
        {
            if (pools[item.name].Count > 0)
            {
                value = pools[item.name].Pop();
            }
        }

        value ??= GameObject.Instantiate(item);
        value.name = item.name;
        value.SetActive(true);
        return value;
    }
}
