using UnityEngine;
using UnityEngine.Events;

public class StarObject : MonoBehaviour
{
    private bool isCollision = false;
    private Rigidbody rigid;
    private MeshRenderer meshRenderer;
    private new Collider collider;

    [SerializeField] private UnityEvent onGetStar;

    private void Awake()
    {
        EventManager.StartListening(Constant.RESET_GAME_EVENT, RegisterStarCount);
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        RegisterStarCount();
    }

    private void Start()
    {
        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, DestroyObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constant.PLAYER_TAG) && GameManager.GameState == GameState.Play && !isCollision)
        {
            Debug.Log("Get Star");
            onGetStar.Invoke();
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

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.RESET_GAME_EVENT, RegisterStarCount);
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, DestroyObject);
    }
}