using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private float kickDamage;
    private float attackDamage;
    private float attack360Damage;
    private float knockbackForce;

    private bool isRageActive;

    // Public ver
    public float KickDamage { get { return kickDamage; } }
    public float AttackDamage { get { return attackDamage; } }
    public float Attack360Damage { get { return attack360Damage; } }
    public float KnockbackForce { get { return knockbackForce; } }
    public bool IsRageActive { get { return isRageActive; } }

    private void Start()
    {
        AnimController.Taunt += RageController;
    }

    void RageController()
    {
        StartCoroutine(RageTimeController(30f));
    }

    IEnumerator RageTimeController(float tauntTime)
    {
        isRageActive = true;
        yield return new WaitForSeconds(tauntTime);
        isRageActive = false;
    }

    private void OnDisable()
    {
        AnimController.Taunt -= RageController;
    }

    public void SetDamageValues()
    {
        kickDamage = isRageActive ? 10f : 5f;
        attackDamage = isRageActive ? 30f : 15f;
        attack360Damage = isRageActive ? 50f : 25f;
        knockbackForce = isRageActive ? 10f : 5f;
    }
}
