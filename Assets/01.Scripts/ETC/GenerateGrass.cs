using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrass : MonoBehaviour
{
    [SerializeField]
    private List<GrassPair> grasses;

    float surfaceY = 0f;
    Vector3 scale;

    private void Start()
    {
        Ready();
        Generate();
    }

    private void Ready()
    {
        scale = transform.localScale;
        surfaceY = transform.position.y + transform.localScale.y / 2f;
    }

    private void Generate()
    {
        foreach (GrassPair pair in grasses)
        {
            for (int i = 0; i < pair.count; i++)
            {
                GameObject obj = Instantiate(pair.grassPrefab, RandomPosition(), RandomRotation(pair.grassPrefab.transform));
                obj.transform.SetParent(transform);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        float x = Mathf.Abs(scale.x) * 0.5f;
        float z = Mathf.Abs(scale.z) * 0.5f;

        float randX = Random.Range(-x, x);
        float randZ = Random.Range(-z, z);

        Vector3 pos = transform.position;
        pos.y = 0f;
        pos += new Vector3(randX, surfaceY, randZ);

        return pos;
    }

    private Quaternion RandomRotation(Transform prefab)
    {
        return Quaternion.Euler(prefab.rotation.eulerAngles.x, Random.Range(0f, 360f), prefab.rotation.eulerAngles.z);
    }
}

[System.Serializable]
public class GrassPair
{
    public GameObject grassPrefab;
    public int count;
}