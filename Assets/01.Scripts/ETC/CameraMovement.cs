using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    private float rotY = 0f;

    [SerializeField] float speed = 3f;

    private void Update()
    {
        if (VerbSystemController.CurrentVerb != null) return;
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            //Lerp ½áµµ ±¦ÂúÀ» µí
            transform.RotateAround(Vector3.zero, Vector3.up, mouseX * Time.deltaTime * speed);
            transform.Rotate(new Vector3(-mouseY, 0f, 0f) * Time.deltaTime * speed);
        }
    }
}