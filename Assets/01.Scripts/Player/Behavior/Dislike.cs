using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dislike : SettingDirection
{
    public override void SetDirection()
    {
        currentCharacter.CurrentDirection += -(target.ItemPosition - transform.position).normalized;
    }
}