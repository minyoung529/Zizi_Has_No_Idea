using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public UIManager UIManager { get; private set; }
    public DataManager Data { get; private set; }

    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        Data = FindObjectOfType<DataManager>();
    }

    void Update()
    {
        
    }
}
