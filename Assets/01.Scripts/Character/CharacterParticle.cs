using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticle;

    public void ActiveParticle(Vector3 velocity)
    {
        velocity.y = 0f;

        if (velocity.sqrMagnitude > 0.1f)
        {
            if (!walkParticle.gameObject.activeSelf)
            {
                walkParticle.gameObject.SetActive(true);
            }
        }
        else
        {
            if (walkParticle.gameObject.activeSelf)
            {
                walkParticle.gameObject.SetActive(false);
            }
        }
    }
}
