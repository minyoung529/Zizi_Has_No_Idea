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
        if (other.CompareTag(Constant.PLATFORM_TAG)) return;

        CharacterMovement character = other.gameObject.GetComponent<CharacterMovement>();

        if (character)
        {
            if (character.IsReverse)
                character.CurrentDirection = -linkedPortal.forward;
            else
                character.CurrentDirection = linkedPortal.forward;
            Vector3 angle = character.transform.eulerAngles;
            angle.x = 0f;
            angle.z = 0f;

            character.transform.eulerAngles = angle;
        }

        other.transform.localPosition = linkedPortal.transform.position;

        if (other.CompareTag(Constant.STAR_TAG))
        {
            other.transform.localPosition += linkedPortal.forward;
        }

        other.GetComponent<Rigidbody>().velocity = Vector3.zero;

        SoundManager.Instance.PlayOneShotAudio(AudioType.EffectSound, portalClip);
    }
}