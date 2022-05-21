using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerbSystemController : MonoBehaviour
{
    [SerializeField] private VerbPanelEvent verbPanelPrefab;

    private static Verb currentVerb;
    public static Verb CurrentVerb { get => currentVerb; set => currentVerb = value; }

    private static Character currentCharacter;
    public static Character CurrentCharacter { get => currentCharacter; set => currentCharacter = value; }

    public static SentencePanel currentPanel;
    public static SentencePanel CurrentPanel { get => currentPanel; set => currentPanel = value; }


    void Start()
    {
        GenerateVerbPanels();
    }

    private void GenerateVerbPanels()
    {
        List<Verb> verbs = GameManager.Instance.Data.Verbs.verbs;

        InitPanel(verbPanelPrefab, verbs[0]);

        for (int i = 1; i < verbs.Count; i++)
        {
            VerbPanelEvent panel = Instantiate(verbPanelPrefab, verbPanelPrefab.transform.parent);
            InitPanel(panel, verbs[i]);
        }
    }

    private void InitPanel(VerbPanelEvent panel, Verb verb)
    {
        panel.Initialize(verb);
        panel.gameObject.SetActive(true);
    }
}
