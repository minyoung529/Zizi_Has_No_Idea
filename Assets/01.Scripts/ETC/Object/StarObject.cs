using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class StarObject : MonoBehaviour
{
    private bool isCollision = false;
    private Rigidbody rigid;
    private MeshRenderer meshRenderer;
    private new Collider collider;

    [SerializeField] private UnityEvent onGetStar;

    private Vector3 originScale;

    private bool isDestroy;
    public bool IsDestroy
    {
        get { return isDestroy; }
        set { isDestroy = value; }
    }

    private void Awake()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, RegisterStarCount);

        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        originScale = transform.localScale;

        rigid.velocity = Vector3.zero;
    }

    private void Start()
    {
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, DestroyObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play && !isCollision)
        {
            onGetStar.Invoke();
            SetData(false);

            GameManager.Instance.StarCount -= 1;
            isCollision = true;

            if (GameManager.Instance.StarCount == 0)
            {
                EventManager.TriggerEvent(Constant.GET_STAR_EVENT);
            }
        }
    }

    private void RegisterStarCount()
    {
        if (rigid == null) return;
        SettingBackPosition();
        SetData(true);
        rigid.velocity = Vector3.zero;
        isCollision = false;

        // 처음에 안 보이는 별이라면
        if (isDestroy)
        {
            DestroyObject();
            return;
        }

        GameManager.Instance.StarCount += 1;
        //Debug.Log($"Star Count = {GameManager.Instance.StarCount}");
    }

    private void SetData(bool isEnabled)
    {
        collider.enabled = isEnabled;
        meshRenderer.enabled = isEnabled;

        if (isEnabled)
        {
            transform.DOKill();
            transform.DOScale(originScale, 0.3f);
        }
    }

    private void DestroyObject()
    {
        //Debug.Log("DestroyStar");
        PoolManager.Push(gameObject);
        isDestroy = false;
    }

    private void SettingBackPosition()
    {
        BackPosition backPosition = GetComponent<BackPosition>();

        if (!IsDestroy)
        {
            if (!backPosition)
            {
                gameObject.AddComponent<BackPosition>();
            }
        }
        else
        {
            if (backPosition)
            {
                Destroy(backPosition);
            }
        }
    }
    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, RegisterStarCount);
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, DestroyObject);
    }
}