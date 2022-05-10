using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    [Header("»∏¿¸")]
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float rotationDeacceleration = 3f;
    [SerializeField] private float mouseSensitivity = 1f;

    private float rotX;
    private float rotY;

    MinMax minMaxAngle = new MinMax(0, 90);

    [Header("¡‹")]
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomAcceleration = 3f;
    private float wheel;

    MinMax minMaxZoom = new MinMax(2, 15);

    [Header("¿Ãµø")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveDeacceleration = 3f;

    private float moveX;
    private float moveY;

    private Camera cam;

    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private float originalview;

    private void Start()
    {
        cam = GetComponent<Camera>();

        originalRotation = transform.rotation;
        originalPosition = transform.position;
        originalview = cam.orthographicSize;

        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetPosAndRot);
    }

    private void Update()
    {
        if (GameManager.GameState == GameState.InGameSetting || GameManager.GameState == GameState.Setting)
            return;

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
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minMaxZoom.min, minMaxZoom.max);
    }

    private void CarryCamera()
    {
        if (Input.GetMouseButton(2))
        {
            moveX = Input.GetAxisRaw("Mouse X");
            moveY = -Input.GetAxisRaw("Mouse Y");
        }
        else
        {
            moveX = Mathf.Lerp(moveX, 0f, Time.deltaTime * moveDeacceleration);
            moveY = Mathf.Lerp(moveY, 0f, Time.deltaTime * moveDeacceleration);
        }

        transform.Translate(new Vector3(-moveX, moveY, 0f) * moveSpeed * Time.deltaTime);
    }

    private void ResetPosAndRot()
    {
        float delay = 1f;

        transform.DORotateQuaternion(originalRotation, delay);
        transform.DOMove(originalPosition, delay);
        cam.DOOrthoSize(originalview, delay);
    }
}