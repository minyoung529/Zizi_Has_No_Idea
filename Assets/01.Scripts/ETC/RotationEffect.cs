using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEffect : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    private float accTime = 0f;
    public float speed = 180f;

    void Update()
    {
        accTime += Time.deltaTime * speed;

        Vector3 eulerAngles = targetTransform.rotation.eulerAngles;
        eulerAngles.z = accTime;
        eulerAngles.y = accTime;
        eulerAngles.x = accTime;

        targetTransform.rotation = Quaternion.Euler(eulerAngles);
    }
}
