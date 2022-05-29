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

    private float accTime = 0f;
    [SerializeField] private float xSpeed = 180f;
    [SerializeField] private float ySpeed = 180f;
    [SerializeField] private float zSpeed = 180f;

    [SerializeField] private RotationType rotationType;


    void Update()
    {
        accTime += Time.deltaTime;

        Vector3 eulerAngles = transform.rotation.eulerAngles;

        if ((rotationType & RotationType.RotationZ) != 0)
        {
            eulerAngles.z = accTime * zSpeed;
        }

        if ((rotationType & RotationType.RotationX) != 0)
        {
            eulerAngles.x = accTime * xSpeed;
        }

        if ((rotationType & RotationType.RotationY) != 0)
        {
            eulerAngles.y = accTime * ySpeed;
        }

        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
