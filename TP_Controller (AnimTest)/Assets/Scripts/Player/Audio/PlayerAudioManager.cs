using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource P_audioSource;

    [SerializeField] private AudioClip Equip;
    [SerializeField] private AudioClip Disarm;
    [SerializeField] private AudioClip Kick;
    [SerializeField] private AudioClip Attack;
    [SerializeField] private AudioClip Attack360;
    [SerializeField] private AudioClip Taunt;
    [SerializeField] private AudioClip Lightning;

    // FootStep
    [SerializeField] private AudioClip stone;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;


    private void Start()
    {
        P_audioSource = GetComponent<AudioSource>();

        AnimController.Equip += PlayEquipSound;
        AnimController.Disarm += PlayDisarmSound;
        AnimController.Kick += PlayKickSound;
        AnimController.Attack += PlayAttackSound;
        AnimController.Attack360 += PlayAttack360Sound;
        AnimController.Taunt += PlayTauntSound;
    }

    private void Update()
    {
        Debug.DrawRay(RayStart.position, RayStart.transform.up * range * -1, Color.green);
    }

    void PlayEquipSound()
    {
        P_audioSource.PlayOneShot(Equip);
    }

    void PlayDisarmSound()
    {
        P_audioSource.PlayOneShot(Disarm);
    }

    void PlayKickSound()
    {
        P_audioSource.PlayOneShot(Kick);
    }

    void PlayAttackSound()
    {
        P_audioSource.PlayOneShot(Attack);
    }

    void PlayAttack360Sound()
    {
        P_audioSource.PlayOneShot(Attack360);
    }

    void PlayTauntSound()
    {
        P_audioSource.PlayOneShot(Taunt);
        StartCoroutine(PlayRageLightningSound());
    }

    IEnumerator PlayRageLightningSound()
    {
        yield return new WaitForSeconds(1);
        P_audioSource.PlayOneShot(Lightning);
    }

    // Animasyona event olarak eklendi.
    public void Footstep()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("stone"))
            {
                PlayfootStepSoundL(stone);
            }
            // Sonradan farklý zeminler için ses eklenebilir halde.
        }
    }

    void PlayfootStepSoundL(AudioClip audio)
    {
        //P_audioSource.pitch = Random.Range(0.8f, 1f);
        P_audioSource.PlayOneShot(audio);
    }

    private void OnDisable()
    {
        AnimController.Equip -= PlayEquipSound;
        AnimController.Disarm -= PlayDisarmSound;
        AnimController.Kick -= PlayKickSound;
        AnimController.Attack -= PlayAttackSound;
        AnimController.Attack360 -= PlayAttack360Sound;
        AnimController.Taunt -= PlayTauntSound;
    }
}
