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
    }

    private void ActiveChartImage(EventParam param)
    {
        float delay = 0.3f;
        if (param.boolean == chartImage.gameObject.activeSelf) return;

        if (param.boolean)
        {
            chartImage.gameObject.SetActive(true);
            chartImage.transform.localScale = Vector3.zero;
            chartImage.transform.DOScale(1f, delay).SetEase(Ease.InOutQuad);
        }
        else
        {
            chartImage.transform.DOScale(0f, delay).SetEase(Ease.InOutQuad)
                .OnComplete(() => chartImage.gameObject.SetActive(false));
        }

        ActiveUnitScroll(false);
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
            x.Init(null);
        });

        Debug.Log(items.Count);

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
            }

            Debug.Log($"Generate => {items[i].Item.Name}");
            panel.gameObject.SetActive(true);
            panel.Init(items[i].Item);
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

    public void ActiveInfoImage(string message)
    {
        verbInfoImage.ActiveMessage(message);
    }
}
