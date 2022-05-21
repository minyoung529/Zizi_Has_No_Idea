using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text stageText;

    [Header("Character Chart")]
    [SerializeField] private RectTransform chartImage;
    [SerializeField] private SentencePanel sentencePanelPrefab;
    private List<SentencePanel> sentencePanels = new List<SentencePanel>();

    [SerializeField] private Image unitScroll;

    [Header("ETC UI")]
    [SerializeField] private InfoImage verbInfoImage;

    void Start()
    {
        Debug.Log("UI Manager Start");
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, ActiveChartImage);
        EventManager.StartListening(Constant.START_PLAY_EVENT, InactiveCharImage);
        EventManager.StartListening(Constant.RESET_GAME_EVENT, () => ChangeStage(GameManager.CurrentStage));
    }

    private void ActiveChartImage(EventParam param)
    {
        if (param.boolean == chartImage.gameObject.activeSelf) return;

        if (param.boolean)
        {
            chartImage.gameObject.SetActive(true);
            chartImage.transform.localScale = Vector3.zero;
            chartImage.transform.DOScale(1f, 0.3f).SetEase(Ease.InOutQuad);
        }
        else
        {
            InactiveCharImage();
        }

        ActiveUnitScroll(false);
    }

    private void InactiveCharImage()
    {
        if (!chartImage.gameObject.activeSelf) return;

        chartImage.transform.DOScale(0f, 0.3f).SetEase(Ease.InOutQuad)
    .OnComplete(() => chartImage.gameObject.SetActive(false));
    }

    public void ChangeStage(int stage)
    {
        stageText.text = $"Stage {stage}";
        GenerateSentencePanels();
    }

    private void GenerateSentencePanels()
    {
        List<ItemObject> items = GameManager.Instance.CurrentItems;

        if (items == null || items.Count == 0) return;

        Debug.Log("Generate Success");

        sentencePanels.ForEach(x =>
        {
            x.gameObject.SetActive(false);
            x.SetData(null);
        });

        for (int i = 0; i < items.Count; i++)
        {
            SentencePanel panel;

            if (i < sentencePanels.Count)
            {
                panel = sentencePanels[i];
            }

            else
            {
                panel = Instantiate(sentencePanelPrefab, sentencePanelPrefab.transform.parent);
                sentencePanels.Add(panel);
                panel.Init(items[i].Item);
            }

            Debug.Log($"Generate => {items[i].Item.Name}");
            panel.gameObject.SetActive(true);
            panel.SetData(items[i].Item);
        }
    }

    public void ActiveUnitScroll(bool isActive)
    {
        unitScroll.gameObject.SetActive(isActive);

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Vector3 pos = EventSystem.current.currentSelectedGameObject.transform.position;
            pos.x -= 135 * 0.5f;
            pos.y -= 50 * 0.5f;
            unitScroll.transform.position = pos;
        }
    }

    public void ActiveInfoImage(string message, Vector3 position)
    {
        verbInfoImage.ActiveMessage(message, position);
    }

    private void OnDestroy()
    {
        EventManager<EventParam>.StopListening(Constant.CLICK_PLAYER_EVENT, ActiveChartImage);
        EventManager.StopListening(Constant.START_PLAY_EVENT, InactiveCharImage);
        EventManager.StopListening(Constant.RESET_GAME_EVENT, () => ChangeStage(GameManager.CurrentStage));
    }
}
