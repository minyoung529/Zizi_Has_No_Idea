using System.Collections;
using UnityEngine;
[System.Serializable]
public class Item
{
    [SerializeField] private string name;
    public string Name { get { return name; } }
    
    [SerializeField] private VerbType verbType;
    public VerbType VerbType { get { return verbType; } set { verbType = value; } }

    private Vector3 itemPosition;
    public Vector3 ItemPosition { get { return itemPosition; } set { itemPosition = value; } }
}
