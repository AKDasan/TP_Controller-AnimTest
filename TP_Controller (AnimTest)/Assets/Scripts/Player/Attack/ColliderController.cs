using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField] private Collider attack360_C;
    [SerializeField] private Collider attack_C;
    [SerializeField] private Collider kick_C;

    private void Awake()
    {
        attack360_C.enabled = false;
        attack_C.enabled = false;
        kick_C.enabled = false;
    }

    void Start()
    {
        AnimController.Kick += Kick;
        AnimController.Attack += Attack;
        AnimController.Attack360 += Attack360;
    }

    void Kick()
    {
        StartCoroutine(KickI());
    }

    void Attack()
    {
        StartCoroutine(AttackI());
    }

    void Attack360()
    {
        StartCoroutine(Attack360I());
    }

    // Kick 1.85f
    // Attack 1.85f
    // Attack360 2.5f

    IEnumerator KickI()
    {
        kick_C.enabled = true;
        yield return new WaitForSeconds(1.85f);
        kick_C.enabled = false;
    }

    IEnumerator AttackI()
    {
        attack_C.enabled = true;
        yield return new WaitForSeconds(1.85f);
        attack_C.enabled = false;
    }

    IEnumerator Attack360I()
    {
        attack360_C.enabled = true;
        yield return new WaitForSeconds(2.5f);
        attack360_C.enabled = false;
    }

    private void OnDisable()
    {
        AnimController.Kick += Kick;
        AnimController.Attack += Attack;
        AnimController.Attack360 += Attack360;
    }
}
