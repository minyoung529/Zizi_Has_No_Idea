using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThornObject : MonoBehaviour
{
    private string PARTICLE_PATH = "BloodParticle";

    private void Start()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, ResetObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag(Constant.PLATFORM_TAG))
        {
            //GameObject particle = PoolManager.Pop(PARTICLE_PATH);
            //particle.transform.SetParent()

            GameManager.Instance.ResetStage();
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        transform.DOMoveY(0f, 0.5f);
    }

    public void Disable()
    {
        transform.DOMoveY(-1f, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void ResetObject()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, ResetObject);
    }
}