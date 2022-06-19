using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunk : SettingDirection
{
    private CharacterMovement characterMovement;
    private const string PARTICLE_PATH = "DrunkParticle";

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    public override void OnCollisionTarget(Collision collision)
    {
        characterMovement.IsReverse = true;

        GameObject particle = PoolManager.Pop(PARTICLE_PATH);
        particle.transform.SetParent(transform);
        particle.transform.localPosition = Vector3.up * 2f;
    }
}
