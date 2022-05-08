using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDirection : MonoBehaviour
{
    protected CharacterMovement currentCharacter;
    protected Item target;
    public bool IsActive { get; set; }

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterMovement>();
    }

    public void Init(Item target)
    {
        this.target = target;
    }

    public virtual void SetDirection() { }

    public virtual void SetupDirection() { }
}
