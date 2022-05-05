using System;
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
        SetPlayerDirection();
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
            items[i] = itemObjects[i].Item;

        currentItems = items;
    }

    public void SetPlayerDirection()
    {
        PlayerMovement.CurrentDirection = Vector3.zero;

        for (int i = 0; i < currentItems.Length; i++)
        {
            if (currentItems[i].VerbType == VerbType.None) continue;
            SetDirection(currentItems[i].VerbType, currentItems[i].ItemPosition);
        }
    }

    private void SetDirection(VerbType type, Vector3 itemPos)
    {
        SettingDirection settingDirection = playerBrainTransform.GetComponent(type.ToString()) as SettingDirection;
        settingDirection ??= playerBrainTransform.gameObject.AddComponent(Type.GetType(type.ToString())) as SettingDirection;
        settingDirection.SetDirection(itemPos);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StopListening(Constant.GET_STAR_EVENT, ClearStage);
    }
}