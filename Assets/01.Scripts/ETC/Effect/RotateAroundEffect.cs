using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundEffect : MonoBehaviour
{
    public enum RotationType
    {
        RotationX,
        RotationY,
        RotationZ
    }

    [SerializeField] private float speed = 1f;

    [SerializeField] private RotationType rotationType;

    void Update()
    {
        if (rotationType == RotationType.RotationZ)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, Time.deltaTime * speed);
        }

        if (rotationType == RotationType.RotationX)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * speed);
        }

        if (rotationType == RotationType.RotationY)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * speed);
        }
    }
}
