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

    private static int currentStage = 1;
    public static int CurrentStage { get { return currentStage; } set { currentStage = value; } }

    private GameObject currentStagePrefab;

    [SerializeField]
    private Character player;


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
    }

    private void ClearStage()
    {
        if (GameState != GameState.Play) return;

        if (currentStagePrefab != null)
        {
            Destroy(currentStagePrefab);
        }

        Debug.Log("Clear Stage");

        GameObject prefab = Data.LoadStage();
        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        ResetStage();
    }

    public void ResetStage()
    {
        Debug.Log("Reset Stage");
        EventManager.TriggerEvent(Constant.RESET_GAME_EVENT);
        GameState = GameState.Ready;

        RegisterCurrentItem();
        UIManager.ChangeStage(currentStage);
        player.transform.position = currentStagePrefab.transform.GetChild(0).transform.position;
    }

    public void RegisterCurrentItem()
    {
        ItemObject[] itemObjects = currentStagePrefab.GetComponentsInChildren<ItemObject>();
        Character[] characters = currentStagePrefab.GetComponentsInChildren<Character>();
        Item[] items = new Item[itemObjects.Length];

        for (int i = 0; i < itemObjects.Length; i++)
        {
            items[i] = itemObjects[i].Item;
            items[i].verbPairs = new Dictionary<Character, VerbType>();
            items[i].verbPairs.Add(player, VerbType.None);

            for (int j = 0; j < characters.Length; j++)
            {
                items[i].verbPairs.Add(characters[j], VerbType.None);
            }
        }

        currentItems = items;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StopListening(Constant.GET_STAR_EVENT, ClearStage);
    }
}