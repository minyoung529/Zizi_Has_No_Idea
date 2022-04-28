using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; private set; } = GameState.Play;

    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }


    private static int currentChapter = 1;
    public static int CurrentChapter { get { return currentChapter; } set { currentChapter = value; } }

    private static int currentStage = 0;
    public static int CurrentStage { get { return currentStage; } set { currentStage = value; } }

    [SerializeField]
    private GameObject currentStagePrefab;

    [SerializeField]
    private Transform playerBrainTransform;


    private Item[] currentItems;
    public Item[] CurrentItems { get { return currentItems; } }

    void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StartListening(Constant.GET_STAR_EVENT, ClearStage);

        ClearStage();
    }

    private void StartPlay()
    {
        Debug.Log("Play Start");
        GameState = GameState.Play;

        playerBrainTransform.forward = CalculatorPlayerDirection();
    }

    private void ClearStage()
    {
        if (GameState != GameState.Play) return;

        if (currentStagePrefab != null)
            Destroy(currentStagePrefab);

        Debug.Log("Clear Stage");
        GameState = GameState.Ready;

        GameObject prefab = Data.LoadStage();

        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        RegisterCurrentItem();
        UIManager.ChangeStage(currentStage);
        playerBrainTransform.position = currentStagePrefab.transform.GetChild(0).transform.position;
    }

    public void RegisterCurrentItem()
    {
        ItemObject[] itemObjects = currentStagePrefab.GetComponentsInChildren<ItemObject>();
        Item[] items = new Item[itemObjects.Length];

        for (int i = 0; i < itemObjects.Length; i++)
        {
            items[i] = itemObjects[i].Item;
        }

        currentItems = items;
    }

    public Vector3 CalculatorPlayerDirection()
    {
        Vector3 direction = Vector3.zero;

        for (int i = 0; i < currentItems.Length; i++)
        {
            if (currentItems[i].VerbType == VerbType.None) continue;
            direction += SetDirection(currentItems[i].VerbType, currentItems[i].ItemPosition);
        }

        if (direction.magnitude < 0.1f)
            return Vector3.forward;

        else
            return direction;
    }

    private Vector3 SetDirection(VerbType type, Vector3 itemPos)
    {
        Vector3 playerPosition = playerBrainTransform.position;

        switch (type)
        {
            case VerbType.Like:
                return (itemPos - playerPosition).normalized;
            case VerbType.Dislike:
                return (playerPosition - itemPos).normalized;
        }

        return Vector3.forward;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StopListening(Constant.GET_STAR_EVENT, ClearStage);
    }
}