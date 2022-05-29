using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticle;

    private void Awake()
    {
        Debug.Log("Listening");
        EventManager.StartListening(Constant.GET_STAR_EVENT, StopParticle);
    }

    public void ActiveParticle(Vector3 velocity)
    {
        velocity.y = 0f;

        if (velocity.sqrMagnitude > 0.1f)
        {
            if (!walkParticle.gameObject.activeSelf)
            {
                Debug.Log("계속 돌고 있는데 true");
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

    public void StopParticle()
    {
        if (GameManager.Instance.StarCount == 0)
        {
            walkParticle.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.GET_STAR_EVENT, StopParticle);
    }
}
