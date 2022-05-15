using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : UIButton
{
    [SerializeField]
    private SentencePanel sentencePanel;

    protected override void OnClicked()
    {
        VerbSystemController.CurrentPanel = sentencePanel;
        GameManager.Instance.UIManager.ActiveUnitScroll(true);
    }
}