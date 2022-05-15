using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitPanel : UIButton
{
    int index = 0;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        GetComponentInChildren<Button>().onClick.AddListener(OnClicked);

        Text nameText = GetComponentInChildren<Text>();

        index = transform.GetSiblingIndex();
        nameText.text = Constant.UNITS_NAME[index];
    }

    protected override void OnClicked()
    {
        VerbSystemController.CurrentPanel.SetUnitType((UnitType)index);
        GameManager.Instance.UIManager.ActiveUnitScroll(false);
    }
}