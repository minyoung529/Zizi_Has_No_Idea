using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentencePanel : PanelBase
{
    private Item item;

    [SerializeField] private Text sentenceText;

    public void Init(Item item)
    {
        this.item = item;
        EventManager<EventParam>.StartListening(Constant.CLICK_PLAYER_EVENT, UpdateUI);
        UpdateUI(new EventParam());
    }

    private void UpdateUI(EventParam param)
    {
        if (param.character == null) return;

        string oPostposition = (item.Name[item.Name.Length - 1] - 0xAC00) % 28 > 0 ? "을" : "를";
        string sPostposition = (param.character?.characterName[param.character.characterName.Length - 1] - 0xAC00) % 28 > 0 ? "은" : "는";
        
        sentenceText.text = $"{param.character?.characterName}{sPostposition} {item.Name}{oPostposition}";
    }

    public void ChangeVerbType(VerbType verbType)
    {
        Debug.Log(item.Name);
        Debug.Log(verbType);
        item.verbPairs[VerbSystemController.CurrentCharacter] = verbType;
        VerbSystemController.CurrentVerb = null;
    }

    void AdjustUIDetail()
    {

    }
}
