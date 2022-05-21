using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StagesSO")]
public class StagesSO : ScriptableObject
{
    public int chapter;
    public List<GameObject> stages;
}