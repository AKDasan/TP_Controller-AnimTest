using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    private AudioSource E_audioSource;

    [SerializeField] private AudioClip TakeDamage;
    [SerializeField] private AudioClip Bleeding;
    [SerializeField] private AudioClip Stunning;
    [SerializeField] private AudioClip KnockBack;

    private void Start()
    {
        E_audioSource = GetComponent<AudioSource>();
    }

    void PlayTakeDamageSound()
    {
        E_audioSource.PlayOneShot(TakeDamage);
    }

    void PlayBleedingSound()
    {
        E_audioSource.PlayOneShot(Bleeding);
    }

    void PlayStunningSound()
    {
        E_audioSource.PlayOneShot(Stunning);
    }

    void PlayKnockBackSound()
    {
        E_audioSource.PlayOneShot(KnockBack);
    }

    private void OnDisable()
    {
        
    }
}
