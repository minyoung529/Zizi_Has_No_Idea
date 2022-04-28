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
        string postposition = (item.Name[item.Name.Length - 1] - 0xAC00) % 28 > 0 ? "À»" : "¸¦";

        sentenceText.text = $"{Constant.PLAYER_NAME}Àº {item.Name}{postposition}";
    }

    public void ChangeVerbType(VerbType verbType)
    {
        item.VerbType = VerbSystemController.CurrentVerb.verbType;
    }

    void AdjustUIDetail()
    {

    }
}
