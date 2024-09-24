using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem rageParticle; // Neck'e child olarak atandý. (Rig)

    private void Awake()
    {
        rageParticle.Stop();
    }

    private void Start()
    {
        AnimController.Taunt += RageParticleController;
    }

    void RageParticleController()
    {
        StartCoroutine(ParticleTimeController(1f,30f, rageParticle));
    }

    IEnumerator ParticleTimeController(float delay,float time, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
        yield return new WaitForSeconds(time);
        particle.Stop();
    }

    private void OnDisable()
    {
        AnimController.Taunt -= RageParticleController;
    }
}
