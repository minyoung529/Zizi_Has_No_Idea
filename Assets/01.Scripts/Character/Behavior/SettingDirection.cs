using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDirection : MonoBehaviour
{
    protected CharacterMovement currentCharacter;
    protected ItemObject target;
    protected Verb verb;
    public SimulateType SimulateType { get => verb.simulateType; } 

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
        currentCharacter ??= GetComponent<CharacterMovement>();
        verb = target.Item.verbPairs[currentCharacter.character];
        SetupDirection();
    }

    public virtual void SetDirection() { }

    public virtual void SetupDirection() { }

    public virtual void ResetData()
    {
        IsActive = false;
        target = null;
    }

    public virtual void OnCollisionTarget(Collision collision) { }

    private void OnCollisionEnter(Collision collision)
    {
        ChildOnCollisionTrigger();

        if (!IsActive || target == null) return;

        if (target.gameObject == collision.gameObject)
        {
            OnCollisionTarget(collision);
        }
    }

    protected virtual void ChildOnCollisionTrigger() { }
}
