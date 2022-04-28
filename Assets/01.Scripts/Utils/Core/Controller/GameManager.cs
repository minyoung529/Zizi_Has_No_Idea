using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static GameState GameState { get; private set; } = GameState.Play;
    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }

    private int currentChapter = 1;
    private int currentStage = 0;

    [SerializeField]
    private GameObject currentStagePrefab;

    private Item[] currentItems;

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
            Destroy(currentStagePrefab);

        Debug.Log("Clear Stage");
        GameState = GameState.Ready;

        GameObject prefab = Data.LoadStage(ref currentChapter, ref currentStage);
        currentStagePrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        RegisterCurrentItem();
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

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.START_PLAY_EVENT, StartPlay);
        EventManager.StopListening(Constant.GET_STAR_EVENT, ClearStage);
    }

}