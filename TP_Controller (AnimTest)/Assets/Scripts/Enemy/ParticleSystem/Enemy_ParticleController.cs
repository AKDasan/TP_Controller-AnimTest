using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem stunParticle;

    private void Awake()
    {
        stunParticle.Stop();
    }

    private void Start()
    {
        var enemyTakeDamageController = GetComponent<EnemyTakeDamageController>();
        if (enemyTakeDamageController != null) 
        {
            enemyTakeDamageController.Stunning += StunningParticleController;
        }
    }

    void StunningParticleController()
    {
        StartCoroutine(ParticleTimeController(0f, 5f, stunParticle));
    }

    IEnumerator ParticleTimeController(float delay, float time, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
        yield return new WaitForSeconds(time);
        particle.Stop();
    }

    private void OnDisable()
    {
        var enemyTakeDamageController = GetComponent<EnemyTakeDamageController>();
        if (enemyTakeDamageController != null)
        {
            enemyTakeDamageController.Stunning -= StunningParticleController;
        }
    }
}
