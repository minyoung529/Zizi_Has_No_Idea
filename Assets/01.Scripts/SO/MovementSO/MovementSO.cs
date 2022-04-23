using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/InGame/Movement")]
public class MovementSO : ScriptableObject
{
    public float speed;
    public float jumpForce;

    public float acceleration;
    public float deacceleration;
}
