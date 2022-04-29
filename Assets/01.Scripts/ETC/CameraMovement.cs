using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("회전")]
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float rotationDeacceleration = 3f;
    [SerializeField] private float mouseSensitivity = 1f;

    private float rotX;
    private float rotY;

    private const float minAngle = 0f;
    private const float maxAngle = 90f;


    [Header("줌")]
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomAcceleration = 3f;
    private float wheel;

    private const float minCameraZoom = 2f;
    private const float maxCameraZoom = 15f;

    [Header("이동")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveDeacceleration = 3f;

    private float moveX;
    private float moveY;


    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (VerbSystemController.CurrentVerb != null) return;

        MouseXYMove();
        MouseScrollWheelZoom();
        CarryCamera();
    }

    private void MouseXYMove()
    {
        if (Input.GetMouseButton(0))
        {
            rotX = Input.GetAxisRaw("Mouse X");
            rotY = Input.GetAxisRaw("Mouse Y");
        }
        else
        {
            rotX = Mathf.Lerp(rotX, 0f, Time.deltaTime * rotationDeacceleration);
            rotY = Mathf.Lerp(rotY, 0f, Time.deltaTime * rotationDeacceleration);
        }

        transform.RotateAround(Vector3.zero, Vector3.up, rotX * Time.deltaTime * rotationSpeed);
        transform.Rotate(new Vector3(-rotY, 0f, 0f) * Time.deltaTime * rotationSpeed);

        //TODO: 카메라 이상한 움직임 고치기
        transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, minAngle, maxAngle),
                                                          transform.rotation.eulerAngles.y,
                                                          transform.rotation.eulerAngles.z);
    }

    private void MouseScrollWheelZoom()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) < 0.1f)
        {
            wheel = Mathf.Lerp(wheel, 0f, Time.deltaTime * zoomAcceleration);
        }
        else
        {
            if (scroll > 0f)
            {
                wheel = Mathf.Lerp(wheel, zoomSpeed, Time.deltaTime * zoomAcceleration);
            }
            else
            {
                wheel = Mathf.Lerp(wheel, -zoomSpeed, Time.deltaTime * zoomAcceleration);
            }
        }

        cam.orthographicSize -= wheel;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minCameraZoom, maxCameraZoom);
    }

    private void CarryCamera()
    {
        if (Input.GetMouseButton(2))
        {
            moveX = Input.GetAxisRaw("Mouse X");
            moveY= Input.GetAxisRaw("Mouse Y");
        }
        else
        {
            moveX = Mathf.Lerp(moveX, 0f, Time.deltaTime * moveDeacceleration);
            moveY = Mathf.Lerp(moveY, 0f, Time.deltaTime * moveDeacceleration);
        }

        transform.Translate(new Vector3(-moveX, moveY, 0f) * moveSpeed * Time.deltaTime);
    }
}