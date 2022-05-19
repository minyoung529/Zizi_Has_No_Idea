using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dislike : SettingDirection
{
    private Rigidbody rigid;

    public override void SetupDirection()
    {
        //currentCharacter.CurrentDirection += -(target.transform.position - transform.position).normalized;
        //rigid = target.transform.GetComponent<Rigidbody>();
    }

    public override void SetDirection()
    {
        //if (rigid == null) return;k
        if (target != null && GameManager.GameState == GameState.Play)
        {
            currentCharacter.CurrentDirection += -(target.transform.position - transform.position).normalized;
        }
    }

}