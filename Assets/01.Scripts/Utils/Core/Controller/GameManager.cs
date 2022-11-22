using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; set; } = GameState.NotGame;

    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }

    public static int CurrentChapter { get; set; } = 1;
    public static int CurrentStage { get; set; } = 0;

    private GameObject currentStagePrefab;

    public List<ItemObject> CurrentItems = new List<ItemObject>();
    public List<Character> CurrentCharacters { get; set; } = new List<Character>();
    public List<VerbType> CurrentVerbs { get; set; }

    private int starCount = 0;
    public int StarCount
    {
        get => starCount;
        set => starCount = value;
    }

    public Transform PlayerTransform => currentStagePrefab.transform.GetChild(0);

    [SerializeField] private bool skipLobbyScene = true;

    // LATER: DELETE THIS
    public bool editorMode = false;
    public int stage;

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();
        PoolManager.Awake();

        EventManager.StartListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StartListening(Constant.GET_STAR_EVENT, () => ClearStage());
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, SetGameState);
        EventManager.StartListening(Constant.GAME_START_EVENT, () => GameState = GameState.Ready);

        ParabolaController.Start();
    }

    private void Start()
    {
        if (editorMode)
            CurrentStage = stage;
        else
        {
            CurrentStage = Data.User.stage;
            Data.User.maxStage = Mathf.Max(Data.User.maxStage, CurrentStage);
        }

        ClearStage(0f);

        if (skipLobbyScene)
        {
            EventManager.TriggerEvent(Constant.GAME_START_EVENT);
        }
    }

    public void GameStart()
    {
        GameState = GameState.Ready;
    }

    private void Update()
    {
        if (!editorMode) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameState = GameState.Play;
            ClearStage(0f);
        }
    }

    private void StartPlay()
    {
        GameState = GameState.Play;
    }

    public void ClearStage(float delay = 3f)
    {
        GameState = GameState.Ready;

        if (delay >= 0.01f)
        {
            SoundManager.Instance.PlayClearSound();
        }

        CurrentVerbs?.Clear();
        starCount = 0;

        if (Data.IsValidStage(CurrentChapter, CurrentStage))
        {
            StartCoroutine(LoadStage(delay));
        }
        else
        {
            CurrentStage = 0;
            UIManager.EndStage();
        }
    }

    private IEnumerator LoadStage(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentStagePrefab != null)
        {
            EventManager.TriggerEvent(Constant.POOL_EVENT);
            Destroy(currentStagePrefab.gameObject);
        }

        yield return new WaitForSeconds(0.2f);

        GameObject prefab = Data.LoadStage();
        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        CurrentItems.Clear();
        CurrentCharacters.Clear();

        EventManager.TriggerEvent(Constant.CLEAR_STAGE_EVENT);
        ResetStage();
        RegisterCurrentItem();

        Debug.Log("Clear Stage");
    }

    public void ResetStage()
    {

        starCount = 0;
        EventManager.TriggerEvent(Constant.RESET_GAME_EVENT);

        if (GameState != GameState.NotGame)
        {
            GameState = GameState.Ready;
        }
    }

    public void ResetStageButton()
    {
        if (GameState != GameState.Play) return;
        ResetStage();
    }

    public void RegisterCurrentItem()
    {
        for (int i = 0; i < CurrentItems.Count; i++)
        {
            CurrentItems[i].Item.verbPairs = new Dictionary<Character, Verb>();

            for (int j = 0; j < CurrentCharacters.Count; j++)
            {
                if (CurrentCharacters[j].gameObject == CurrentItems[i].gameObject) continue;

                Verb defaultVerb = Data.Verbs.verbs[0];
                CurrentItems[i].Item.verbPairs.Add(CurrentCharacters[j], new Verb(defaultVerb));
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
        EventManager.StopListening(Constant.GET_STAR_EVENT, () => ClearStage());
        EventManager.StopListening(Constant.GAME_START_EVENT, () => GameState = GameState.Ready);
        EventManager<EventParam>.StopListening(Constant.CLICK_PLAYER_EVENT, SetGameState);
    }
}