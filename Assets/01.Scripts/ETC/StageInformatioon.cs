using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInformatioon : MonoBehaviour
{
    public List<VerbType> activeVerbs;

    private void Awake()
    {
        GameManager.Instance.CurrentVerbs = activeVerbs;
    }
}
