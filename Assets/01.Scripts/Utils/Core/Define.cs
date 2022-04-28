using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    private static Camera mainCam;
    public static Camera MainCam
    {
        get
        {
            mainCam ??= Camera.main;
            return mainCam;
        }
    }

    public static Vector3 MousePosition
    {
        get
        {
            return MainCam.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x, Input.mousePosition.y, MainCam.farClipPlane));
        }
    }

    private static Transform playerTransform;
    public static Transform PlayerTransform
    {
        get
        {
            playerTransform ??= FindObjectOfType<PlayerMovement>().transform;
            return playerTransform;
        }
    }
}
