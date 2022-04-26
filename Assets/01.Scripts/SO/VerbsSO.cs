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
}