using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Like : SettingDirection
{
    private Rigidbody rigid;

    public override void SetupDirection()
    {
        //currentCharacter.CurrentDirection += (target.transform.position - transform.position).normalized;
        //rigid = target.transform.GetComponentInChildren<Rigidbody>();
    }

    public override void SetDirection()
    {
        if (GameManager.GameState == GameState.Play && target != null)
        {
            currentCharacter.CurrentDirection += (target.transform.position - transform.position).normalized;
        }
    }
}
