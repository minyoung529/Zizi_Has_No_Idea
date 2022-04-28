using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform chartImage;

    [SerializeField] private Text stageText;

    [SerializeField] private SentencePanel sentencePanelPrefab;
    private List<SentencePanel> sentencePanels = new List<SentencePanel>();

    void Start()
    {
        Debug.Log("UI Manager Start");
        EventManager<bool>.StartListening(Constant.CLICK_PLAYER_EVENT, ActiveChartImage);
    }

    private void ActiveChartImage(bool isActive)
    {
        float delay = 0.3f;
        if (isActive == chartImage.gameObject.activeSelf) return;

        if (isActive)
        {
            Vector3 pos = Input.mousePosition;
            pos.x -= 100f;
            pos.y -= 70f;

            chartImage.anchoredPosition = pos;

            chartImage.gameObject.SetActive(true);
            chartImage.transform.localScale = Vector3.zero;
            chartImage.transform.DOScale(1f, delay).SetEase(Ease.InOutQuad);
        }
        else
        {
            chartImage.transform.DOScale(0f, delay).SetEase(Ease.InOutQuad)
                .OnComplete(() => chartImage.gameObject.SetActive(false));
        }
    }

    public void ChangeStage(int stage)
    {
        stageText.text = $"Stage {stage}";
        GenerateSentencePanels();
    }

    private void GenerateSentencePanels()
    {
        Item[] items = GameManager.Instance.CurrentItems;

        if (items == null || items.Length == 0) return;

        for (int i = 0; i < items.Length; i++)
        {
            SentencePanel panel;

            if (i < sentencePanels.Count)
                panel = sentencePanels[i];

            else
            {
                panel = Instantiate(sentencePanelPrefab, sentencePanelPrefab.transform.parent);
                sentencePanels.Add(panel);
            }

            panel.gameObject.SetActive(true);
            panel.Init(items[i]);
        }
    }
}
