using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : MonoBehaviour
{
    [SerializeField] private Transform linkedPortal;

    private void OnCollisionEnter(Collision collision)
    {
        CharacterMovement character = collision.gameObject.GetComponent<CharacterMovement>();

        if(character)
        {
            character.CurrentDirection = linkedPortal.forward;
            character.transform.localPosition = linkedPortal.transform.position;
        }
    }
}