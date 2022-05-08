using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;

    private void Start()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetCharacter);
    }

    private void ResetCharacter()
    {
        transform.rotation = Quaternion.identity;
    }
}
