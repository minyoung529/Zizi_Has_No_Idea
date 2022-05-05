using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Like : SettingDirection
{
    public override void SetDirection(Vector3 targets)
    {
        PlayerMovement.CurrentDirection += (targets - transform.position).normalized;
    }
}
