using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/VerbsSO")]
public class VerbsSO : ScriptableObject
{
    public List<Verb> verbs;
}

[System.Serializable]
public class Verb
{
    public string verbName;
    public Sprite verbSprites;
    public VerbType verbType;
    public SimulateType simulateType;
    public bool hasUnit;
    public string postposition;

    public UnitType unitType { get; set; } = UnitType.Middle;

    public Verb(Verb verb)
    {
        ChangeData(verb);
    }

    public void ChangeData(Verb verb)
    {
        verbName = verb.verbName;
        verbSprites = verb.verbSprites;
        verbType = verb.verbType;
        simulateType = verb.simulateType;
        hasUnit = verb.hasUnit;
        unitType = verb.unitType;
        postposition = verb.postposition;
    }
}