using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageController : MonoBehaviour
{
    private GameObject player;
    private DamageController damageController;

    [SerializeField] private float Health;

    public delegate void RageAction(float rageValue);
    public static event RageAction isDamaged;
    private bool isDamageable;

    [SerializeField] private bool isBleeding;
    [SerializeField] private bool isStunning;

    public event Action Stunning;

    // EnemyType
    [SerializeField] private EnemyTypeSO enemyType;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("Player bulunamadý.");
        }
    }

    private void Start()
    {
        damageController = FindObjectOfType<DamageController>();

        Health = enemyType.Health;

        isDamageable = true;
        isBleeding = false;
        isStunning = false;
    }

    private void Update()
    {
        damageController.SetDamageValues();
        EnemyStatusController();
        HealthController();
    }

    void HealthController()
    {
        if (Health <= 0) Health = enemyType.Health;
    }

    void EnemyStatusController()
    {
        if (!enemyType.isBleedImmune)
        {
            if (isBleeding) { Health -= 1f * Time.deltaTime; }
        }
        
        if (!enemyType.isStunningImmune)
        {
            if (isStunning) { } // Hareket edemeyecek. 
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDamageable)
        {
            if (other.gameObject.CompareTag("Attack"))
            {
                TakeAttack(damageController.AttackDamage);
            }
            else if (other.gameObject.CompareTag("Attack360"))
            {
                TakeAttack(damageController.Attack360Damage);
                if (!isBleeding) { StartCoroutine(BleedingController()); }             
            }
            else if (other.gameObject.CompareTag("Kick"))
            {
                TakeAttack(damageController.KickDamage);
                if (!isStunning) { StartCoroutine(StunningController()); }
                if (!enemyType.isKnockBackImmune) { Knockback(); }
            }
            StartCoroutine(DamageableController());
        }
    }

    void TakeAttack(float damage)
    {
        Health -= damage;
    }

    void Knockback()
    {
        float knockbackForce = damageController.KnockbackForce;
        Vector3 knockbackDirection = transform.position - player.transform.position;
        knockbackDirection.y = 0;
        knockbackDirection.Normalize();

        if (TryGetComponent(out Rigidbody enemyRB))
        {
            enemyRB.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("EnemyRB bulunamadý!");
        }
    }

    IEnumerator BleedingController()
    {
        if (!enemyType.isBleedImmune)
        {
            isBleeding = true;
            yield return new WaitForSeconds(10f);
            isBleeding = false;
        }     
    }

    IEnumerator StunningController()
    {
        if (!enemyType.isStunningImmune)
        {
            Stunning?.Invoke();
            isStunning = true;
            yield return new WaitForSeconds(5f);
            isStunning = false;
        }    
    }

    IEnumerator DamageableController()
    {
        isDamaged?.Invoke(50f);
        isDamageable = false;
        yield return new WaitForSeconds(1.5f);
        isDamageable = true;
    }
}
