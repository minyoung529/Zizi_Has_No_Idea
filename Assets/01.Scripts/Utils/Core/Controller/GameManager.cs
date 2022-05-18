using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; private set; } = GameState.Play;

    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }


    public static int CurrentChapter { get; set; } = 1;
    public static int CurrentStage { get; set; } = 7;

    private GameObject currentStagePrefab;

    public List<ItemObject> CurrentItems = new List<ItemObject>();
    public List<Character> CurrentCharacters { get; set; } = new List<Character>();

    private int starCount = 0;
    public int StarCount
    {
        get => starCount;
        set
        {
            starCount = value;
            Debug.Log(starCount);
        }
    }

    public Transform PlayerTransform => currentStagePrefab.transform.GetChild(0);

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameState = GameState.Play;
            ClearStage();
        }
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
            Destroy(currentStagePrefab.gameObject);
        }

        StartCoroutine(LoadStage());
    }

    private IEnumerator LoadStage()
    {
        yield return new WaitForSeconds(0.15f);

        GameObject prefab = Data.LoadStage();
        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        CurrentItems.Clear();
        CurrentCharacters.Clear();

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
        UIManager.ChangeStage(CurrentStage);
    }

    public void RegisterCurrentItem()
    {
        for (int i = 0; i < CurrentItems.Count; i++)
        {
            CurrentItems[i].Item.verbPairs = new Dictionary<Character, Verb>();

            for (int j = 0; j < CurrentCharacters.Count; j++)
            {
                if (CurrentCharacters[j].gameObject == CurrentItems[i].gameObject) continue;
                CurrentItems[i].Item.verbPairs.Add(CurrentCharacters[j], new Verb());
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