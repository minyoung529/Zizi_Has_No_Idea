using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private User user;
    public User User { get { return user; } set { user = value; } }

    private string SAVE_PATH = "";
    private const string SAVE_FILE = "/SaveFile.Json";

    [SerializeField] private List<StagesSO> chapterDatas;

    [SerializeField] private VerbsSO verbs;
    public VerbsSO Verbs { get { return verbs; } }

    public ParabolaObject parabolaPrefab;

    void Awake()
    {
        DontDestroyOnLoad(this);

        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();

        EventManager.StartListening(Constant.CLEAR_STAGE_EVENT, SaveUser);
    }

    #region Json
    private void LoadFromJson()
    {
        User data;

        if (File.Exists(SAVE_PATH + SAVE_FILE))
        {
            string stringJson = File.ReadAllText(SAVE_PATH + SAVE_FILE);
            data = JsonUtility.FromJson<User>(stringJson);
        }
        else
        {
            data = new User();
        }

        user = data;
        SaveToJson(data);
    }

    public void SaveToJson<T>(T data)
    {
        string stringJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILE, stringJson, System.Text.Encoding.UTF8);
    }
    #endregion

    public GameObject LoadStage()
    {
        List<GameObject> stages = chapterDatas.Find(x => x.chapter == GameManager.CurrentChapter).stages;

        if (GameManager.CurrentStage >= stages.Count)
        {
            GameManager.CurrentStage = 0;
            GameManager.CurrentChapter++;
        }
        else
        {
            GameManager.CurrentStage++;
        }

        return stages[GameManager.CurrentStage - 1];
    }

    public bool IsValidStage(int chapter, int stage)
    {
        bool isValid = stage < chapterDatas[chapter - 1].stages.Count;
        return isValid;
    }

    private void SaveUser()
    {
        User newUser = new User();
        newUser.stage = Mathf.Clamp(GameManager.CurrentStage - 1, 0, chapterDatas[0].stages.Count);
        newUser.maxStage = Mathf.Max(newUser.stage, user.maxStage);

        SaveToJson(newUser);
    }

    private void OnApplicationQuit()
    {
        SaveUser();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constant.CLEAR_STAGE_EVENT, SaveUser);
    }
}
