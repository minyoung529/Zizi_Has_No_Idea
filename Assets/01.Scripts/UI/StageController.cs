using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private StagePanel stagePanel;
    [SerializeField]
    private StagesSO stages;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        CreateStagePanel();
    }

    private void CreateStagePanel()
    {
        if (stages.stages.Count == 0) return;

        for (int i = 0; i < stages.stages.Count - 1; i++)
        {
            Instantiate(stagePanel, stagePanel.transform.parent);
        }
    }

    public void ActiveStagePanel()
    {
        GameManager.GameState  = GameState.InGameSetting;

        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, 0.5f);
    }

    public void InactiveStagePanel()
    {
        GameManager.GameState = GameState.Ready;

        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
