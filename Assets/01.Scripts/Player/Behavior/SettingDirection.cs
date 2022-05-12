using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDirection : MonoBehaviour
{
    protected CharacterMovement currentCharacter;
    protected ItemObject target;
    public bool IsActive { get; set; }

    protected bool isStopMovement = false;
    public bool IsStopMovement { get => isStopMovement; }

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

    public virtual void OnCollisionTarget() { }

    private void OnCollisionEnter(Collision collision)
    {
        if(target.gameObject == collision.gameObject)
        {
            OnCollisionTarget();
        }
    }
}
