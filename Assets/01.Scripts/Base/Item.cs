using System.Collections;
using UnityEngine;
[System.Serializable]
public class Item
{
    [SerializeField] private string name;
    public string Name { get { return name; } }
    
    [SerializeField] public VerbType verbType;
    public VerbType VerbType { get { return verbType; } }
}
