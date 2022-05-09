using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Like : SettingDirection
{
    public override void SetDirection()
    {
        currentCharacter.CurrentDirection += (target.transform.position - transform.position).normalized;
    }
}
