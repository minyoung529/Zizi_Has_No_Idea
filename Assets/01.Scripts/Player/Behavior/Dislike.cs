using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dislike : SettingDirection
{
    public override void SetDirection(Vector3 targets)
    {
        PlayerMovement.CurrentDirection += (transform.position - targets).normalized;
    }
}