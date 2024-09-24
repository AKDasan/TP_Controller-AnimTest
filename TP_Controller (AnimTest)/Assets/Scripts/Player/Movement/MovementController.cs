using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private Transform TP_camera;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnTime = 0.1f;
    float turnVelocity;

    private Vector3 direction;

    public float DirectionMagnitude
    {
        get { return direction.magnitude; }
        set { direction = value * direction.normalized; }
    }

    private bool isWalking = false;
    private bool isRunning = false;

    public bool IsWalking { get { return isWalking; } }
    public bool IsRunning { get { return isRunning; } }

    private AnimController animController;

    private void Awake()
    {
        if (!TryGetComponent(out animController))
        {
            Debug.Log("AnimController bulunamadý.");
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
         if (animController.CanMove)
         {
             float horizontal = Input.GetAxisRaw("Horizontal");
             float vertical = Input.GetAxisRaw("Vertical");

             direction = new Vector3(horizontal, 0, vertical).normalized;

             if (direction.magnitude >= 0.1f)
             {
                 float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + TP_camera.eulerAngles.y;
                 float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
                 transform.rotation = Quaternion.Euler(0f, angle, 0f);

                 Vector3 movedirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                 MovementSpeedController();
                 characterController.Move(movedirection.normalized * moveSpeed * Time.deltaTime);
             }
         }

        //Debug.Log(characterController.velocity.magnitude);
    }

    void MovementSpeedController()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isWalking = false;
            isRunning = true;
            moveSpeed = 5f;

        }
        else
        {
            isWalking = true;
            isRunning = false;
            moveSpeed = 3f;
        }
    }
}
