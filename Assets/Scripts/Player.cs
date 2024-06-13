using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Rigidbody rigid;
    Animator animator;
    ParticleSystem ps;

    private RotateToMouse rotateToMouse;

    public int keyCount = 0;

    readonly int IsMoveHash = Animator.StringToHash("isMove");
    readonly int IsDieHash = Animator.StringToHash("isDie");

    public float basicMoveSpeed = 5.0f;

    public float dashPower = 1.0f;

    public float realMoveSpeed = 0.0f;

    float moveFB = 0.0f;
    float moveLR = 0.0f;

    public int KeyCount
    {
        get => keyCount;
        set
        {
            if(keyCount != value)
            {
                keyCount = Math.Min(value, 10);
                onKeyCountChange?.Invoke(keyCount);
            }
        }
    }

    public Action<int> onKeyCountChange;

    public Action<int> onDie;

    private void Awake()
    {
        inputActions = new();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rotateToMouse = GetComponent<RotateToMouse>();
        Transform child = transform.GetChild(5);
        ps = child.GetComponent<ParticleSystem>();

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Boom.performed += OnBoomInput;
        inputActions.Player.Boom.canceled += OnBoomInput;
    }


    private void OnDisable()
    {
        inputActions.Player.Boom.canceled -= OnBoomInput;
        inputActions.Player.Boom.performed -= OnBoomInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Disable();
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        SetInput(context.ReadValue<Vector2>(), !context.canceled);

    }

    private void OnBoomInput(InputAction.CallbackContext context)
    {
        ps.gameObject.SetActive(true);
    }

    private void Update()
    {
        UpdateRotate();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }
    void SetInput(Vector2 input, bool isMove)
    {
        moveFB = input.y;
        moveLR = input.x;

        animator.SetBool(IsMoveHash, isMove);

    }

    void Move()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * basicMoveSpeed * moveFB * transform.forward);
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * basicMoveSpeed * moveLR * transform.right);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            KeyCount++;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            GameClear();
        }
    }

    void OnDie()
    {
        inputActions.Player.Disable();
        onDie?.Invoke(KeyCount);
        GameManager.Instance.Die();
        Cursor.lockState = CursorLockMode.None;
        animator.SetTrigger(IsDieHash);
    }
    void GameClear()
    {
        if(keyCount == 10)
        {
            inputActions.Player.Disable();
            GameManager.Instance.Clear();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
