using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotationEffect : MonoBehaviour
{
    [Flags]
    public enum RotationType
    {
        RotationX = 1,
        RotationY = 2,
        RotationZ = 4
    }

    [SerializeField] Transform targetTransform;
    private float accTime = 0f;
    [SerializeField] private float xSpeed = 180f;
    [SerializeField] private float ySpeed = 180f;
    [SerializeField] private float zSpeed = 180f;

    [SerializeField] private RotationType rotationType;

    private void Start()
    {
        targetTransform ??= transform;
    }

    void Update()
    {
        accTime += Time.deltaTime;

        Vector3 eulerAngles = targetTransform.rotation.eulerAngles;

        if ((rotationType & RotationType.RotationZ) != 0)
        {
            if (gameObject.name == "Point(Clone)")
                Debug.Log(1);
            eulerAngles.z = accTime * zSpeed;
        }

        if ((rotationType & RotationType.RotationX) != 0)
        {
            if (gameObject.name == "Point(Clone)")
                Debug.Log(1);

            eulerAngles.x = accTime * xSpeed;
        }

        if ((rotationType & RotationType.RotationY) != 0)
        {
            eulerAngles.y = accTime * ySpeed;
        }

        targetTransform.rotation = Quaternion.Euler(eulerAngles);
    }
}
