using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string name;
    public string Name { get { return name; } }

    public Dictionary<Character, VerbType> verbPairs;

    private Vector3 itemPosition;
    public Vector3 ItemPosition { get { return itemPosition; } set { itemPosition = value; } }
}