using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaController
{
    public static void GenerateParabola(Transform startPoint, Transform endPoint)
    {
        ParabolaObject obj = GameObject.Instantiate(GameManager.Instance.Data.parabolaPrefab, null);
        obj.Init(startPoint, endPoint);
        obj.gameObject.SetActive(true);
    }
}
