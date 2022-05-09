using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaObject : MonoBehaviour
{
    private Transform target1;
    private Transform target2;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private LineRenderer lineRenderer;

    public int count = 30;

    public float maxHeight = 60f;

    private void Init(Transform target1, Transform target2)
    {
        this.target1 = target1;
        this.target2 = target2;

        lineRenderer.SetPosition(0, target1.position + Vector3.up * 0.5f);
        lineRenderer.SetPosition(count - 1, target2.position + Vector3.up * 0.5f);

        startPoint = lineRenderer.GetPosition(0);
        endPoint = lineRenderer.GetPosition(count - 1);

        DrawParabola();
    }

    private void DrawParabola()
    {
        for (int i = 1; i < count - 1; i++)
        {
            Vector3 position = Vector3.zero;
            float increment = (i - 1) / ((float)count - 2);

            float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
            position.y = maxHeight * sin + startPoint.y;

            if (Mathf.Abs(endPoint.x - startPoint.x) > 0.01f)
                position.x = increment * (endPoint.x - startPoint.x);

            if (Mathf.Abs(endPoint.z - startPoint.z) > 0.01f)
                position.z = increment * (endPoint.z - startPoint.z);

            lineRenderer.SetPosition(i, position);
        }
    }

    public ParabolaObject(Transform target1, Transform target2)
    {
        lineRenderer ??= gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = count;

        Init(target1, target2);
    }
}
