using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBehavior : MonoBehaviour
{
    protected CharacterMovement currentCharacter;
    protected ItemObject target;
    protected Verb verb;
    public SimulateType SimulateType { get => verb.simulateType; } 

    public bool IsActive { get; set; }

    protected bool isStopMovement = false;
    public bool IsStopMovement { get => isStopMovement; }

    protected virtual void Awake()
    {
        currentCharacter = GetComponent<CharacterMovement>();
    }

    public void Init(ItemObject target)
    {
        this.target = target;
        IsActive = true;
        currentCharacter ??= GetComponent<CharacterMovement>();
        verb = target.Item.verbPairs[currentCharacter.character];
    }

    /// <summary>
    /// 플레이 하고 있는 동안 계속 Direction을 설정하는 함수
    /// </summary>
    public virtual void SetDirection() { }

    public virtual void ResetData()
    {
        IsActive = false;
        target = null;
    }

    /// <summary>
    /// Target(item)과 부딪혔을 때 자식에서 재정의할 함수
    /// </summary>
    public virtual void OnCollisionTarget(Collision collision) { }

    /// <summary>
    /// collision과 부딪혔을 때 자식에서 재정의할 함수
    /// </summary>
    protected virtual void ChildOnCollisionTrigger(Collision collision) { }

    private void OnCollisionEnter(Collision collision)
    {
        ChildOnCollisionTrigger(collision);

        if (!IsActive || target == null) return;

        if (target.gameObject == collision.gameObject)
        {
            OnCollisionTarget(collision);
        }
    }
}
