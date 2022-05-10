using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dislike : SettingDirection
{
    public override void SetupDirection()
    {
        currentCharacter.CurrentDirection += -(target.transform.position - transform.position).normalized;
    }
}