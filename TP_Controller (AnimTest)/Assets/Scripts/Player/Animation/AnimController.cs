using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] private bool isArmed;
    public bool Armed { get; private set; }

    [SerializeField] private bool isEquip;

    private MovementController movementController;

    private bool isCoroutineActive;

    // Block
    private bool isBlocking = false;
    public bool Blocking { get { return isBlocking; } }

    // Bu bool false ise animsyonlar oynatýlýrken player hareket edemez!!!
    [SerializeField] private bool isCanMove;
    public bool CanMove { get { return isCanMove; } }

    // Events
    public static event Action Equip;
    public static event Action Disarm;
    public static event Action Kick;
    public static event Action Attack;
    public static event Action Attack360;
    public static event Action Taunt;

    public delegate void RageDecreaseAction(float rageValue);
    public static event RageDecreaseAction RageActive;

    private P_status_Controller p_Status_Controller;

    private void Awake()
    {
        if (!TryGetComponent(out movementController))
        {
            Debug.LogError("MovementController bulunamadý!");
        }

        p_Status_Controller = GetComponent<P_status_Controller>();
    }

    private void Start()
    {
        isArmed = false;
        isEquip = false;
        isCanMove = true;
        isCoroutineActive = false;
    }

    void Update()
    {
        MovementAnimController();

        if (!isCoroutineActive)
        {
            EquipAndDisarmAnimController();
        }
    }

    void MovementAnimController()
    {
        if (movementController.DirectionMagnitude >= 0.15f)
        {
            if (isArmed)
            {
                KickAnimController();
                AttackAnimController();
                Attack360AnimController();
                TauntAnimController();
                BlockAnimController();
                animator.SetBool("isArmed", false);
                animator.SetBool("isArmedWalk", movementController.IsWalking);
                animator.SetBool("isArmedRun", movementController.IsRunning);
            }
            else
            {
                animator.SetBool("isUnarmed", false);
                animator.SetBool("isUnarmedWalk", movementController.IsWalking);
                animator.SetBool("isUnarmedRun", movementController.IsRunning);
            }
        }
        else
        {
            if (isArmed)
            {
                KickAnimController();
                AttackAnimController();
                Attack360AnimController();
                TauntAnimController();
                BlockAnimController();
                animator.SetBool("isUnarmed", false);
                animator.SetBool("isArmed", true);
                animator.SetBool("isArmedWalk", false);
                animator.SetBool("isArmedRun", false);
            }
            else
            {
                animator.SetBool("isArmed", false);
                animator.SetBool("isUnarmed", true);
                animator.SetBool("isUnarmedWalk", false);
                animator.SetBool("isUnarmedRun", false);
            }
        }
    }

    void EquipAndDisarmAnimController()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCoroutineActive)
            {
                StartCoroutine(MagnitudeController());
                animator.SetBool("isUnarmedWalk", false);
                animator.SetBool("isUnarmedRun", false);
                animator.SetBool("isArmedWalk", false);
                animator.SetBool("isArmedRun", false);

                if (isArmed)
                {
                    animator.SetBool("isArmed", true);
                }
                else
                {
                    animator.SetBool("isUnarmed", true);
                }

                isArmed = !isArmed;
                isEquip = !isEquip;

                if (isEquip)
                {
                    Equip?.Invoke();
                    StartCoroutine(CanMoveControllerEquip(1.25f));
                    animator.SetBool("isEquip", true);
                    animator.SetBool("isDisarm", false);
                }
                else
                {
                    Disarm?.Invoke();
                    StartCoroutine(CanMoveControllerEquip(1.5f));
                    animator.SetBool("isDisarm", true);
                    animator.SetBool("isEquip", false);
                }
            }
        }
    }

    void KickAnimController()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isCoroutineActive && !isBlocking)
            {
                StartCoroutine(CanMoveControllerKick(1.85f));
                animator.SetTrigger("Kick");
            }
        }
    }

    void AttackAnimController()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isCoroutineActive && !isBlocking)
            {
                StartCoroutine(CanMoveControllerAttack(1.85f));
                animator.SetTrigger("Attack");
            }
        }
    }

    void Attack360AnimController()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isCoroutineActive && !isBlocking)
            {
                StartCoroutine(CanMoveControllerAttack360(2.50f));
                animator.SetTrigger("Attack360");
            }
        }
    }

    void TauntAnimController()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isCoroutineActive && !isBlocking && p_Status_Controller.RageValue == 100)
            {
                RageActive?.Invoke(100f);
                StartCoroutine(CanMoveControllerTaunt(3f));
                animator.SetTrigger("Taunt");
            }
        }
    }

    void BlockAnimController()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !isCoroutineActive)
        {
            if (!isBlocking)
            {
                isBlocking = true;
                animator.SetBool("Block", true);
                isCanMove = false;
            }
        }
        else if (isBlocking)
        {
            isBlocking = false;
            animator.SetBool("Block", false);
            StartCoroutine(CanMoveControllerBlock(0.5f));
        }
    }

    IEnumerator CanMoveControllerEquip(float Animtime)
    {
        isCoroutineActive = true;
        isCanMove = !isCanMove;
        yield return new WaitForSeconds(Animtime);
        isCanMove = !isCanMove;
        isCoroutineActive = false;
    }

    IEnumerator CanMoveControllerKick(float Animtime)
    {
        Kick?.Invoke();
        isCoroutineActive = true;
        isCanMove = !isCanMove;
        yield return new WaitForSeconds(Animtime);
        isCanMove = !isCanMove;
        isCoroutineActive = false;
    }

    IEnumerator CanMoveControllerAttack(float Animtime)
    {
        Attack?.Invoke();
        isCoroutineActive = true;
        isCanMove = !isCanMove;
        yield return new WaitForSeconds(Animtime);
        isCanMove = !isCanMove;
        isCoroutineActive = false;
    }

    IEnumerator CanMoveControllerAttack360(float Animtime)
    {
        Attack360?.Invoke();
        isCoroutineActive = true;
        isCanMove = !isCanMove;
        yield return new WaitForSeconds(Animtime);
        isCanMove = !isCanMove;
        isCoroutineActive = false;
    }

    IEnumerator CanMoveControllerTaunt(float Animtime)
    {
        Taunt?.Invoke();
        isCoroutineActive = true;
        isCanMove = !isCanMove;
        yield return new WaitForSeconds(Animtime);
        isCanMove = !isCanMove;
        isCoroutineActive = false;
    }

    IEnumerator CanMoveControllerBlock(float Animtime)
    {
        yield return new WaitForSeconds(Animtime);
        isCanMove = true;
        isCoroutineActive = false;
    }

    IEnumerator MagnitudeController()
    {
        movementController.DirectionMagnitude = 0;
        yield return new WaitForSeconds(1f);
    }
}