using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentencePanel : PanelBase
{
    private Item item;
    private WordImageVisual worldImage;
    [SerializeField] private Text sentenceText;

    public void Init(Item item)
    {
        this.item = item;
        worldImage ??= GetComponentInChildren<WordImageVisual>();

        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, UpdateUI);
        EventManager<VerbType>.StartListening(Constant.SELECT_VERB_WORD, ChangeVerbType);

        UpdateUI(new EventParam());
    }

    private void UpdateUI(EventParam param)
    {
        if (param.character == null || param.character?.characterName == item.Name)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        string oPostposition = (item.Name[item.Name.Length - 1] - 0xAC00) % 28 > 0 ? "을" : "를";
        string sPostposition = (param.character?.characterName[param.character.characterName.Length - 1] - 0xAC00) % 28 > 0 ? "은" : "는";

        worldImage.SetSprite(item);

        sentenceText.text = $"{param.character?.characterName}{sPostposition} {item.Name}{oPostposition}";
    }

    public void ChangeVerbType(VerbType verbType)
    {
        if (Vector2.Distance(worldImage.transform.position, Input.mousePosition) > 50f) return;

        Debug.Log(item.Name);
        Debug.Log(verbType);
        item.verbPairs[VerbSystemController.CurrentCharacter] = verbType;
        VerbSystemController.CurrentVerb = null;

        ItemObject itemObj = GameManager.Instance.CurrentItems.Find(x => x.Item.Name == item.Name);
        ParabolaController.GenerateParabola(VerbSystemController.CurrentCharacter, itemObj);
    }

    void AdjustUIDetail()
    {

    }
}
