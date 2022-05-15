using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentencePanel : PanelBase
{
    private Item item;
    private WordImageVisual worldImage;
    private Verb verb;
    [SerializeField] private Text subjectText;
    [SerializeField] private Text unitText;
    [SerializeField] private Text verbText;

    private RectTransform unitRect;
    private RectTransform verbRect;
    private RectTransform panelRect;

    private float offset = 410f;

    private Vector2 originalSizeDelta;

    public void Init(Item item)
    {
        this.item = item;
        worldImage ??= GetComponentInChildren<WordImageVisual>();

        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, UpdateUI);
        EventManager.StartListening(Constant.SELECT_VERB_WORD, ChangeVerbType);

        verb = GameManager.Instance.Data.Verbs.verbs.Find(x => x.verbType == VerbType.None);

        SetUI();
        UpdateUI();
    }

    private void UpdateUI(EventParam param = new EventParam())
    {
        if (param.character == null || param.character?.characterName == item.Name)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        string oPostposition = (item.Name[item.Name.Length - 1] - 0xAC00) % 28 > 0 ? "을" : "를";
        string sPostposition = (param.character?.characterName[param.character.characterName.Length - 1] - 0xAC00) % 28 > 0 ? "은" : "는";
        subjectText.text = $"{param.character?.characterName}{sPostposition} {item.Name}{oPostposition} ";

        worldImage.SetSprite(item);


        AdjustTextDetail();
    }

    public void ChangeVerbType()
    {
        if (Vector2.Distance(worldImage.transform.position, Input.mousePosition) > 50f) return;

        verb = VerbSystemController.CurrentVerb;
        item.verbPairs[VerbSystemController.CurrentCharacter] = verb.verbType;
        VerbSystemController.CurrentVerb = null;

        ItemObject itemObj = GameManager.Instance.CurrentItems.Find(x => x.Item.Name == item.Name);
        ParabolaController.GenerateParabola(VerbSystemController.CurrentCharacter, itemObj);

        AdjustTextDetail();
        Debug.Log(item.Name + ", " + verb.verbType);
    }

    void AdjustTextDetail()
    {
        Vector2 startPoint = subjectText.rectTransform.anchoredPosition;
        startPoint.x += subjectText.text.Length * subjectText.fontSize;

        Vector2 nextPos = new Vector2(46f, subjectText.rectTransform.anchoredPosition.y - 75f);

        if (verb.isHasUnit)
        {
            ArrangeText(startPoint, unitRect, ref nextPos);

            startPoint = unitRect.anchoredPosition;
            startPoint.x = unitText.text.Length * unitText.fontSize + unitRect.anchoredPosition.x + unitRect.rect.width;
        }

        ArrangeText(startPoint, verbRect, ref nextPos, true);

        unitRect.gameObject.SetActive(verb.isHasUnit);
    }

    private void ArrangeText(Vector3 startPoint, RectTransform transform, ref Vector2 nextPos, bool isSetRect = false)
    {
        Vector2 pos = startPoint;
        transform.anchoredPosition = pos;

        if (transform.anchoredPosition.x > offset)
        {
            transform.anchoredPosition = nextPos;
            nextPos = transform.anchoredPosition;
            nextPos.x += unitText.text.Length * unitText.fontSize + unitRect.rect.width;

            if (isSetRect)
                panelRect.sizeDelta = originalSizeDelta + Vector2.up * 75f;
        }
    }

    private void SetUI()
    {
        unitRect = unitText.transform.parent.GetComponent<RectTransform>();
        verbRect = verbText.transform.parent.GetComponent<RectTransform>();

        panelRect = GetComponent<RectTransform>();
        originalSizeDelta = panelRect.sizeDelta;
    }
}
