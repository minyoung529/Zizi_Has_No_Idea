using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StagePanel : PanelBase
{
    int Stage { get => transform.GetSiblingIndex() + 1; }
    private Button button;

    void Awake()
    {
        Text numberText = GetComponentInChildren<Text>();
        numberText.text = (transform.GetSiblingIndex() + 1).ToString();

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);

        SetInteractable();
    }

    private void OnClicked()
    {
        if (GameManager.CurrentStage == Stage)
            return;


        GameManager.CurrentStage = transform.GetSiblingIndex();
        GameManager.Instance.ClearStage(0.1f);
    }

    private void SetInteractable()
    {
        if (!button) return;

        if (Stage > GameManager.Instance.Data.User.maxStage)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;

            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.white;
            button.colors = colorBlock;
        }
    }

    private void OnEnable()
    {
        SetInteractable();
    }
}