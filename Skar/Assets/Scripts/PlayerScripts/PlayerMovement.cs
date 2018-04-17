using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StatesManager))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour {

    [Serializable]
    public class InputSettings
    {
        public bool showCursor;
        public CursorLockMode lockState = CursorLockMode.Locked;
        public float horizontal;
        public float vertical;
        public float mouseX;
        public float mouseY;

        public void CursorSettings(bool visibility, CursorLockMode lockState)
        {
            Cursor.lockState = lockState;
            Cursor.visible = visibility;
        }

        public Vector2 GetInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        public void GetAxis()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }

        public Vector3 GetMoveDirection(Vector2 input, Transform camAnchor)
        {
            Vector3 v;
            Vector3 h;
            v = camAnchor.forward;
            h = camAnchor.right;
            v.y = 0;
            h.y = 0;
            return (v * input.y + h * input.x);
        }
    }

    [Serializable]
    public class MovementSettings
    {
        public float speed = 5;
        public float jumpForce = 10;
        public int midAirJumps = 1;
        [Range(0,1)]
        public float rotationSpeed = .4f;
        public float groundedDrag = 10;
        public bool midairControl;
        public bool lockOnOverride;
    }

    [SerializeField] float timeTillNextJump = .5f;
    private float jumpTime;
    [SerializeField] private float currentNumberOfJumps;

    [Serializable]
    public class AdvancedSettings
    {
        public float groundCheckDistance = .06f;
        public Vector3 groundCheckoffset;
        public LayerMask ignoredGrounds;
    }

    public InputSettings inputSettings = new InputSettings();
    public MovementSettings movementSettings = new MovementSettings();
    public AdvancedSettings advancedSettings = new AdvancedSettings();
    AbilitesManager abilites;
    private StatesManager states;
    [Range(0,1)]

    Rigidbody rb;
    CapsuleCollider playerCollider;
    private bool onGround;

    RaycastHit groundHit;
    public float gravityMultiplier = 2;
    // Use this for initialization
    void Start () {
        inputSettings.CursorSettings(inputSettings.showCursor, inputSettings.lockState);
        rb = GetComponent<Rigidbody>();
        rb.drag = movementSettings.groundedDrag;
        playerCollider = GetComponentInChildren<CapsuleCollider>();

        states = GetComponent<StatesManager>();
        abilites = GetComponent<AbilitesManager>();
    }

    private void Update()
    {
        inputSettings.CursorSettings(inputSettings.showCursor, inputSettings.lockState);
    }

    private void FixedUpdate()
    {
        inputSettings.GetAxis();
        CheckGround();
        AirborneCheck();
        AddGravitaionalForce();
        if (movementSettings.midairControl)
        {
            ExecuteMovement();
        }
        else
        {
            if (onGround)
            {
                ExecuteMovement();
            }
        }
    }
    void AddGravitaionalForce(){
       if(rb.useGravity){
           rb.AddForce(Physics.gravity * gravityMultiplier);
       }
    }
    public void RotateToMoveDirection(Vector3 direction , float speed )
    {
        Quaternion desiredRot = Quaternion.LookRotation(direction);
	    transform.rotation = Quaternion.Slerp(transform.rotation,desiredRot, speed);
    }

    void Move(Vector3 direction)
    {
        if (rb.velocity.magnitude < movementSettings.speed)
        {
            rb.AddForce(direction * movementSettings.speed * rb.mass, ForceMode.Impulse);
        }
        else
        {
            rb.velocity = new Vector3(direction.x * movementSettings.speed, rb.velocity.y, direction.z * movementSettings.speed);
        }
    }

    void ExecuteMovement()
    {
        Vector3 moveDirection;
        if (Mathf.Abs(inputSettings.horizontal) > 0 || Mathf.Abs(inputSettings.vertical) > 0)
        {
            moveDirection = inputSettings.GetMoveDirection(inputSettings.GetInput(), Camera.main.transform);
            if (Mathf.Abs(inputSettings.horizontal) > 0 && Mathf.Abs(inputSettings.vertical) > 0)
            {
                Move(Vector3.ProjectOnPlane((moveDirection * Mathf.Sin(Mathf.Deg2Rad * 45)) , groundHit.normal));
            }
            else
            {
                Move(moveDirection);
            }

            if (!movementSettings.lockOnOverride)
            {
                RotateToMoveDirection(moveDirection, movementSettings.rotationSpeed);
            }
        }
    }

    void CheckGround()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - advancedSettings.groundCheckDistance, transform.position.z));
        if (Physics.Raycast(transform.position + advancedSettings.groundCheckoffset, Vector3.down, out groundHit, advancedSettings.groundCheckDistance,~advancedSettings.ignoredGrounds,QueryTriggerInteraction.Ignore))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        states.isGrounded = onGround;
        Debug.DrawLine(transform.position, groundHit.point, Color.red);
    }

    void AirborneCheck()
    {
        if(onGround)
        {
            rb.drag = movementSettings.groundedDrag;
            currentNumberOfJumps = 0;
            Jump();
        }
        else
        {
            if (!abilites.isInDash)
            {
                rb.drag = 0;
            }

            if (currentNumberOfJumps <= movementSettings.midAirJumps)
            {
                if(Time.time > jumpTime + timeTillNextJump)
                {
                    Jump();
                }
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * movementSettings.jumpForce * rb.mass, ForceMode.Impulse);
            jumpTime = Time.time;
            currentNumberOfJumps++;
        }
    }
} 