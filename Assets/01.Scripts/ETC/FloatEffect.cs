using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    private float curAngle = 0f;
    public float height;
    public float speed;

    void Update()
    {
        curAngle += Time.deltaTime * speed;

        if(curAngle >= 360f)
            curAngle = 0f;

        float y = Mathf.Sin(curAngle * Mathf.Deg2Rad);
        transform.position += (Vector3.up * y * height) * Time.deltaTime;
    }
}
