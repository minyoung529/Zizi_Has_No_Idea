using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaController
{
    public const float offsetY = 1.1f;

    private static List<ParabolaObject> parabolas;

    public static void Start()
    {
        EventManager.StartListening(Constant.START_PLAY_EVENT, Reset);
    }

    public static void GenerateParabola(Character startPoint, ItemObject endPoint)
    {
        Check(startPoint, endPoint);

        ParabolaObject obj = GameObject.Instantiate(GameManager.Instance.Data.parabolaPrefab, null);
        obj.Init(startPoint, endPoint);
        obj.gameObject.SetActive(true);

        parabolas.Add(obj);
    }

    private static void Check(Character startPoint, ItemObject endPoint)
    {
        ParabolaObject obj = parabolas.Find(x => x.CharacterName == startPoint.characterName && x.ItemName == endPoint.Item.Name);
        if (obj == null) return;

        GameObject.Destroy(obj.gameObject);
    }

    private static void Reset()
    {
        foreach(ParabolaObject obj in parabolas)
        {
            GameObject.Destroy(obj.gameObject);
        }

        parabolas.Clear();
    }
}