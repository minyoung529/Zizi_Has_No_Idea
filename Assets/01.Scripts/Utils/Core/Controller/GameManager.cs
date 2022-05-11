using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; private set; } = GameState.Play;

    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }


    public static int currentChapter { get; set; } = 1;
    public static int currentStage { get; set; } = 4;

    private GameObject currentStagePrefab;

    private List<ItemObject> currentItems;
    public List<ItemObject> CurrentItems { get => currentItems; }

    public List<Character> CurrentCharacters { get; set; }

    public Transform PlayerTransform
        => currentStagePrefab.transform.GetChild(0);

    void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StartListening(Constant.GET_STAR_EVENT, ClearStage);
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, SetGameState);

        ParabolaController.Start();
    }

    private void Start()
    {
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

        GameObject prefab = Data.LoadStage();
        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        currentItems = new List<ItemObject>();
        CurrentCharacters = new List<Character>();

        EventManager.TriggerEvent(Constant.CLEAR_STAGE_EVENT);

        ResetStage();

        Debug.Log("Clear Stage");
    }

    public void ResetStage()
    {
        Debug.Log("Reset Stage");
        EventManager.TriggerEvent(Constant.RESET_GAME_EVENT);
        GameState = GameState.Ready;

        RegisterCurrentItem();
        UIManager.ChangeStage(currentStage);
    }

    public void RegisterCurrentItem()
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            currentItems[i].Item.verbPairs = new Dictionary<Character, VerbType>();

            for (int j = 0; j < CurrentCharacters.Count; j++)
            {
                if (CurrentCharacters[j].characterName == currentItems[i].Item.Name) continue;
                currentItems[i].Item.verbPairs.Add(CurrentCharacters[j], VerbType.None);
            }
        }
    }

    private void SetGameState(EventParam eventParam)
    {
        if (eventParam.boolean)
            GameState = GameState.InGameSetting;

        else if (GameState == GameState.InGameSetting)
            GameState = GameState.Ready;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StopListening(Constant.GET_STAR_EVENT, ClearStage);
    }
}