using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public static Transform playerTrnasform;
    public static Transform PlayerTransform
    {
        get
        {
            if (playerTrnasform == null)
            {
                playerTrnasform = GameObject.FindGameObjectWithTag(Constant.PLAYER_TAG).transform;
            }

            return playerTrnasform;
        }
    }

    public static Camera mainCam;
    public static Camera MainCam
    {
        get
        {
            if (mainCam == null)
            {
                mainCam = Camera.main;
            }

            return mainCam;
        }
    }
}
