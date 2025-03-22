using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator animator;

    [SerializeField] private float minimumSpeed = 15f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private float turnSmoothTime = 0.25f;
    float turnSmoothVelocity;

    Vector3 velocity;

    [SerializeField] private float groundDistance = 0.65f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool canMove = true;

    [SerializeField] private int physAttackPoints = 5;
    [SerializeField] private int yellAttackPoints = 2;
    [SerializeField] private int manaPoints = 5;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Gravity();
        Attack();
        Jump();

        canMove = false;

        // Проверяем завершение анимации атаки и крика
        if (!IsAnimationPlaying("Attacking") && !IsAnimationPlaying("Yelling"))
        {
            canMove = true;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _isGrounded)
        {
            inputMagnitude *= 2;
        }

        float speed = inputMagnitude * minimumSpeed;

        if (direction.magnitude >= 0.1f && canMove) 
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y; 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
        animator.SetFloat("inputMagnitude", inputMagnitude, 0.1f, Time.deltaTime);
    }

    private void Gravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, groundMask); // ������� ��������� ����� �������� Distance � ������� �������, ��������� �������� �� ����� � Mask 
        animator.SetBool("isGrounded", _isGrounded);

        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && _isGrounded && canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Console.WriteLine("jump");
            animator.SetBool("isJumping", true);

        }

        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void Attack()
    {
        // Обычная атака
        if (Input.GetMouseButtonDown(0) && !IsAnimationPlaying("Attacking") && !IsAnimationPlaying("Yelling") && _isGrounded) // Анимация запускается только при нажатии
        {
            canMove = false;
            animator.SetTrigger("Attack"); // Используем триггер для анимации атаки
        }

        // Атака "yelling"
        if (Input.GetMouseButtonDown(1) && !IsAnimationPlaying("Yelling") && !IsAnimationPlaying("Attacking") && _isGrounded) // Анимация запускается только при нажатии
        {
            canMove = false;
            ManaSystem manaSystem = GetComponent<ManaSystem>();
            if (manaSystem != null && manaSystem.GetCurrentMana() > 0)
            {
                if (manaSystem.UseSufficientMana(manaPoints)) {
                    animator.SetTrigger("Yell"); // Используем триггер для анимации "yelling"
                    FindObjectOfType<AudioManager>().Play("Scream");
                }
            }
        } 
    }



    // Переменная, вызывающаяся в аниматоре
    // Не используется
    public void EnableMovement()
    {
        canMove = true;
    }

    // Метод для проверки, проигрывается ли анимация
    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isPlaying = stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f;
        return isPlaying;
    }
}
