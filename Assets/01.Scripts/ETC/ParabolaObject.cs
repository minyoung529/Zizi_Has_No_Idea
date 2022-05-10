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

    public int count = 100;

    public float maxHeight = 60f;

    private WaitForSeconds drawDelay = new WaitForSeconds(0.05f);

    public void Init(Transform start, Transform end)
    {
        this.target1 = start;
        this.target2 = end;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;

        startPoint = start.position;
        startPoint.y = ParabolaController.offsetY;
        lineRenderer.SetPosition(0, startPoint);

        endPoint = end.position;
        endPoint.y = ParabolaController.offsetY;

        gameObject.SetActive(true);

        StartCoroutine(DrawParabolaObject());
    }

    private void DrawParabola()
    {

    }

    private IEnumerator DrawParabolaObject()
    {
        yield return null;

        for (int i = 1; i < count - 1; i++)
        {
            Vector3 position = Vector3.zero;
            float increment = (i - 1) / ((float)count - 2);

            float sin = Mathf.Sin(increment * 180f * Mathf.Deg2Rad);
            position.y = maxHeight * sin + startPoint.y;

            if (Mathf.Abs(endPoint.x - startPoint.x) > 0.01f)
                position.x = increment * (endPoint.x - startPoint.x) + startPoint.x;

            if (Mathf.Abs(endPoint.z - startPoint.z) > 0.01f)
                position.z = increment * (endPoint.z - startPoint.z) + startPoint.z;

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(i, position);

            yield return drawDelay;
        }

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(count - 1, endPoint);
    }
}
