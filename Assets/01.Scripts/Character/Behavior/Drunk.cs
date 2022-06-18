using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunk : SettingDirection
{
    private CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    public override void OnCollisionTarget(Collision collision)
    {
        characterMovement.IsReverse = true;
    }
}
