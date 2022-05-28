using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StagePanel : PanelBase
{
    void Start()
    {
        Text numberText = GetComponentInChildren<Text>();
        numberText.text = (transform.GetSiblingIndex() + 1).ToString();

        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        if (GameManager.CurrentStage == transform.GetSiblingIndex() + 1)
            return;

        GameManager.CurrentStage = transform.GetSiblingIndex();
        GameManager.Instance.ClearStage();
    }
}