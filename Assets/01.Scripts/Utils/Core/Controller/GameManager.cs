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

    private int starCount = 0;
    public int StarCount
    {
        get => starCount;
        set => starCount = value;
    }

    public Transform PlayerTransform => currentStagePrefab.transform.GetChild(0);

    [SerializeField] private bool skipLobbyScene = true;

    // LATER: DELETE THIS
    public int stage;

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StartListening(Constant.GET_STAR_EVENT, () => ClearStage());
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, SetGameState);
        EventManager.StartListening(Constant.GAME_START_EVENT, () => GameState = GameState.Ready);

        ParabolaController.Start();
    }

    private void Start()
    {
        CurrentStage = stage;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameState = GameState.Play;
            ClearStage(0f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            List<Verb> verbs = new List<Verb>();
            foreach (var item in CurrentItems)
            {
                string s = "";

                s += item.Item.Name + '\n';

                foreach (var pair in item.Item.verbPairs)
                {
                    if (pair.Value.verbType == VerbType.FlyAway)
                    {
                        verbs.Add(pair.Value);
                    }
                    s += pair.Key + "\t" + pair.Value.unitType + '\n';
                }

                Debug.Log(s);
            }
            Debug.Log(verbs[0] == verbs[1]);
        }
    }

    private void StartPlay()
    {
        GameState = GameState.Play;
    }

    public void ClearStage(float delay = 3f)
    {
        if (delay >= 0.01f)
        {
            SoundManager.Instance.PlayClearSound();
        }

        starCount = 0;

        StartCoroutine(LoadStage(delay));
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
        Debug.Log("Reset Stage");
        EventManager.TriggerEvent(Constant.RESET_GAME_EVENT);

        if (GameState != GameState.NotGame)
        {
            GameState = GameState.Ready;
        }
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