using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [field:SerializeField]
    public bool isCharacter { get; set; } = true;
    [field: SerializeField]
    public bool isItem { get; set; } = false;

}
