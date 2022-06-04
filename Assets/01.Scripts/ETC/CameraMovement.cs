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

    MinMax minMaxAngle = new MinMax(0, 90);

    [Header("¡‹")]
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomAcceleration = 3f;
    private float wheel;

    private float rotX;
    private float rotY;

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
        EventManager.StartListening(Constant.GAME_START_EVENT, ResetPosAndRot);
    }

    private void Update()
    {
        if (GameManager.GameState == GameState.NotGame)
        {
            rotX = -0.15f;
            RotateX();
            return;
        }

        else if (!(GameManager.GameState == GameState.Play || GameManager.GameState == GameState.Ready)) return;

        // INPUT CAMERA MOVE

        if (Input.GetMouseButton(0))
        {
            rotX = Input.GetAxis("Mouse X");
            rotY = Input.GetAxis("Mouse Y");
        }

        RotateX();
        RotateY();

        MouseScrollWheelZoom();
        CarryCamera();
    }

    private void RotateX()
    {
        if (!Input.GetMouseButton(0))
        {
            rotX = Mathf.Lerp(rotX, 0f, Time.deltaTime * rotationDeacceleration);
        }

        transform.RotateAround(Vector3.zero, Vector3.up, rotX * Time.deltaTime * rotationSpeed);
    }

    private void RotateY()
    {
        if (!Input.GetMouseButton(0))
        {
            rotY = Mathf.Lerp(rotY, 0f, Time.deltaTime * rotationDeacceleration);
        }

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

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetPosAndRot);
        EventManager.StopListening(Constant.GAME_START_EVENT, ResetPosAndRot);
    }
}