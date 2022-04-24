using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; private set; }
    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }

    private int currentChapter = 1;
    private int currentStage = 1;

    [SerializeField]
    private GameObject currentStagePrefab;

    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StartListening(Constant.GET_STAR_EVENT, ClearStage);
    }

    private void StartPlay()
    {
        Debug.Log("Play Start");
        GameState = GameState.Play;
    }

    private void ClearStage()
    {
        if (GameState != GameState.Play) return;

        Destroy(currentStagePrefab);
        GameState = GameState.Ready;

        GameObject prefab = Data.LoadStage(ref currentChapter, ref currentStage);
        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StopListening(Constant.GET_STAR_EVENT, ClearStage);
    }
}