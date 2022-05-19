using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarObject : MonoBehaviour
{
    private bool isCollision = false;
    private Rigidbody rigid;
    private MeshRenderer meshRenderer;
    private new Collider collider;

    private void Awake()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, RegisterStarCount);
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        RegisterStarCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play && !isCollision)
        {
            Debug.Log("Get Star");

            SetData(false);

            GameManager.Instance.StarCount -= 1;
            isCollision = true;

            if (GameManager.Instance.StarCount == 0)
            {
                Debug.Log(GameManager.Instance.StarCount);
                EventManager.TriggerEvent(Constant.GET_STAR_EVENT);
            }
        }
    }

    private void RegisterStarCount()
    {
        if (rigid == null) return;

        GameManager.Instance.StarCount += 1;
        SetData(true);
        rigid.velocity = Vector3.zero;
        isCollision = false;
        Debug.Log($"Star Count = {GameManager.Instance.StarCount}");
    }

    private void SetData(bool isEnabled)
    {
        collider.enabled = isEnabled;
        meshRenderer.enabled = isEnabled;

        if(isEnabled)
        {
            transform.localScale = Vector3.one;
        }
    }
}
