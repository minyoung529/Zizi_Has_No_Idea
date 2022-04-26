using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerbSystemController : MonoBehaviour
{
    [SerializeField] private VerbPanel verbPanelPrefab;

    private static Verb currentVerb;
    public static Verb CurrentVerb { get { return currentVerb; } set { currentVerb = value; } }

    void Start()
    {
        GenerateVerbPanels();
    }

    private void GenerateVerbPanels()
    {
        List<Verb> verbs = GameManager.Instance.Data.Verbs.verbs;

        for (int i = 0; i < verbs.Count; i++)
        {
            VerbPanel panel = Instantiate(verbPanelPrefab, verbPanelPrefab.transform.parent);
            panel.Initialize(verbs[i]);
            panel.gameObject.SetActive(true);
        }

        Destroy(verbPanelPrefab.gameObject);
    }
}
