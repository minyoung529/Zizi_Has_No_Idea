using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private User user;
    public User User { get { return user; } }

    private string SAVE_PATH = "";
    private const string SAVE_FILE = "/SaveFile.Json";

    [SerializeField] private List<StagesSO> chapterDatas;

    [SerializeField] private VerbsSO verbs;
    public VerbsSO Verbs { get { return verbs; } }

    void Start()
    {
        DontDestroyOnLoad(this);

        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson(user);
    }

    #region Json
    private void LoadFromJson<T>(T data)
    {
        if (File.Exists(SAVE_PATH + SAVE_FILE))
        {
            string stringJson = File.ReadAllText(SAVE_PATH + SAVE_FILE);
            data = JsonUtility.FromJson<T>(stringJson);
        }
        else
        {
            data = default(T);
        }

        SaveToJson(data);
    }

    public void SaveToJson<T>(T data)
    {
        string stringJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILE, stringJson, System.Text.Encoding.UTF8);
    }
    #endregion

    public GameObject LoadStage(ref int chapter, ref int stage)
    {
        int c = chapter;
        int s = 0;

        List<Stage> stages = chapterDatas.Find(x => x.chapter == c).stages;

        if(stage >= stages.Count)
        {
            stage = 0;
            chapter++;
        }
        else
        {
            stage++;
        }

        s = stage;

        return stages.Find(x => x.stageNum == s).stagePrefab;
    }

    private void OnDestroy()
    {
        SaveToJson(user);
    }
}
