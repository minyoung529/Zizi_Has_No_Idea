using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    void Awake()
    {
        GameObject obj = PoolManager.Pop(prefab);
        obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        obj.transform.SetParent(transform);
    }
}
