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
        if (target != null && GameManager.GameState == GameState.Play)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y = 0f;
            Vector3 charPos = transform.position;
            charPos.y = 0f;

            Vector3 direction = (charPos - targetPos).normalized;
            direction.y = 0f;

            currentCharacter.CurrentDirection += direction;
        }
    }

}