using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; private set; }
    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }

    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();

        EventManager.StartListening(Constant.START_PLAY_EVENT, StartPlay);
    }

    private void StartPlay()
    {
        Debug.Log("Play Start");
        GameState = GameState.Play;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
    }
}