using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDirection : MonoBehaviour
{
    protected CharacterMovement currentCharacter;
    protected ItemObject target;
    public bool IsActive { get; set; }

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterMovement>();
    }

    public void Init(ItemObject target)
    {
        this.target = target;
        IsActive = true;
        SetupDirection();
    }

    public virtual void SetDirection() { }

    public virtual void SetupDirection() { }

    public virtual void ResetData()
    {
        IsActive = false;
        target = null;
    }
}
