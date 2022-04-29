using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float deacceleration = 3f;

    private const float minAngle = 0f;
    private const float maxAngle = 90f;

    private void Update()
    {
        if (VerbSystemController.CurrentVerb != null) return;

        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
        }
        else
        {
            mouseX = Mathf.Lerp(mouseX, 0f, Time.deltaTime * deacceleration);
            mouseY = Mathf.Lerp(mouseY, 0f, Time.deltaTime * deacceleration);
        }

        transform.RotateAround(Vector3.zero, Vector3.up, mouseX * Time.deltaTime * rotationSpeed);
        transform.Rotate(new Vector3(-mouseY, 0f, 0f) * Time.deltaTime * rotationSpeed);

        //TODO: 카메라 이상한 움직임 고치기
        transform.rotation = Quaternion.Euler(Mathf.Clamp(  transform.rotation.eulerAngles.x, minAngle, maxAngle),
                                                            transform.rotation.eulerAngles.y,
                                                            transform.rotation.eulerAngles.z);
    }
}