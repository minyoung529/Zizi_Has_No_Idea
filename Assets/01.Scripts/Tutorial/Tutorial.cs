using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Tutorial 
{
    public int targetChapter;
    public int targetStage;
    public Sprite[] tutorialImages;

    public List<TutorialAction> tutorialActions;
}

[System.Serializable]
public class TutorialAction
{
    public int order;
    public string actionName; 
}