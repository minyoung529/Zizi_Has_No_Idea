using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentencePanel : PanelBase
{
    private WordImage wordImage;
    [SerializeField] private Text sentenceText;

    void Start()
    {
        wordImage = GetComponentInChildren<WordImage>();
    }

    void AdjustUIDetail()
    {

    }
}
