using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform target1;
    public Transform target2;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private LineRenderer lineRenderer;

    public int count = 30;

    //1 2 3 4 5
    //1 2 3 2 1

    public float maxHeight = 60f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = count;
        lineRenderer.SetPosition(0, target1.position + Vector3.up * 0.5f);
        lineRenderer.SetPosition(count - 1, target2.position + Vector3.up * 0.5f);

        startPoint = lineRenderer.GetPosition(0);
        endPoint = lineRenderer.GetPosition(count - 1);

        int halfCount = Mathf.RoundToInt(count * 0.5f);

        for (int i = 1; i < count - 1; i++)
        {
            Vector3 position = Vector3.zero;
            float increment = (i - 1) / ((float)count - 2);

            if (i < halfCount)
            {
                float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
                position.y = maxHeight * sin + startPoint.y;
                Debug.Log(i + ",  Inc: " + increment + ", Sin: " + sin);
            }
            else
            {
                float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
                position.y = maxHeight * sin + startPoint.y;
                Debug.Log(i + ",  Inc: " + increment + ", Sin: " + sin);
            }

            if (Mathf.Abs(endPoint.x - startPoint.x) > 0.01f)
                position.x = increment * (endPoint.x - startPoint.x);

            if (Mathf.Abs(endPoint.z - startPoint.z) > 0.01f)
                position.z = increment * (endPoint.z - startPoint.z);

            lineRenderer.SetPosition(i, position);
        }
    }
}
