using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerbSystemController : MonoBehaviour
{
    [SerializeField] private VerbPanelEvent verbPanelPrefab;

    private static Verb currentVerb;
    public static Verb CurrentVerb { get { return currentVerb; } set { currentVerb = value; } }

    private static Character currentCharacter;
    public static Character CurrentCharacter { get { return currentCharacter; } set { currentCharacter = value; } }

    public static SentencePanel CurrentPanel { get; set; }


    void Start()
    {
        GenerateVerbPanels();
    }

    private void GenerateVerbPanels()
    {
        List<Verb> verbs = GameManager.Instance.Data.Verbs.verbs;

        for (int i = 0; i < verbs.Count; i++)
        {
            VerbPanelEvent panel = Instantiate(verbPanelPrefab, verbPanelPrefab.transform.parent);
            panel.Initialize(verbs[i]);
            panel.gameObject.SetActive(true);
        }

        Destroy(verbPanelPrefab.gameObject);
    }
}
