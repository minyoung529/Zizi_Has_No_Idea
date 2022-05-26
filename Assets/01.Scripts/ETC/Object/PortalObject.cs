using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : MonoBehaviour
{
    [SerializeField] private Transform linkedPortal;
    private AudioClip portalClip;

    private void Start()
    {
        portalClip = Resources.Load<AudioClip>("Explosion");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (linkedPortal == null) return;
        if (Vector3.Dot(other.transform.position, transform.position) < 0f) return;

        CharacterMovement character = other.gameObject.GetComponent<CharacterMovement>();

        if(character)
        {
            character.CurrentDirection = linkedPortal.forward;
            character.transform.localPosition = linkedPortal.transform.position;
            SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, portalClip);
        }
    }
}