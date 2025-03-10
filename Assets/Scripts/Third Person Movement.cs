using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController _characterController;
    public Transform _camera;
    public Transform _groundCheck;
    public Animator _animator;

    public float _minimumSpeed = 10f;
    public float _gravity = -9.81f;
    public float _jumpHeight = 3f;

    // ������� �������� ������
    public float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    // ��� �������� �������
    Vector3 _velocity;

    public float _groundDistance = 0.4f;
    public LayerMask _groundMask;

    public bool _isGrounded;
    public bool _canMove = true;

    public int physAttackPoints = 5;
    public int yellAttackPoints = 2;
    public int manaPoints = 5;

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

        _canMove = false;

        // Проверяем завершение анимации атаки и крика
        if (!IsAnimationPlaying("Attacking") && !IsAnimationPlaying("Yelling"))
        {
            _canMove = true;
        }
    }

    private void Move()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        Vector3 _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        float _inputMagnitude = Mathf.Clamp01(_direction.magnitude);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && _isGrounded)
        {
            _inputMagnitude *= 2;
        }

        float _speed = _inputMagnitude * _minimumSpeed;

        if (_direction.magnitude >= 0.1f && _canMove) // ���������, ��������� ��, ��������� ����� ������� �����������
        {
            // ���� �������� ������. atan2 - �������, ������������ ���� ����� ���� � �������� �����������
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y; // ��������� � ������� � ��������� �������� ������ �� y 
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, _angle, 0f);

            Vector3 _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _characterController.Move(_moveDirection.normalized * _speed * Time.deltaTime);

        }
        _animator.SetFloat("inputMagnitude", _inputMagnitude, 0.1f, Time.deltaTime);
    }

    private void Gravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask); // ������� ��������� ����� �������� Distance � ������� �������, ��������� �������� �� ����� � Mask 
        _animator.SetBool("isGrounded", _isGrounded);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        // �������
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && _isGrounded && _canMove)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            Console.WriteLine("jump");
            _animator.SetBool("isJumping", true);

        }

        else
        {
            _animator.SetBool("isJumping", false);
        }
    }

    private void Attack()
    {
        // Обычная атака
        if (Input.GetMouseButtonDown(0) && !IsAnimationPlaying("Attacking") && !IsAnimationPlaying("Yelling") && _isGrounded) // Анимация запускается только при нажатии
        {
            _canMove = false;
            _animator.SetTrigger("Attack"); // Используем триггер для анимации атаки
        }

        // Атака "yelling"
        if (Input.GetMouseButtonDown(1) && !IsAnimationPlaying("Yelling") && !IsAnimationPlaying("Attacking") && _isGrounded) // Анимация запускается только при нажатии
        {
            _canMove = false;
            ManaSystem manaSystem = GetComponent<ManaSystem>();
            if (manaSystem != null && manaSystem.GetCurrentMana() > 0)
            {
                if (manaSystem.UseSufficientMana(manaPoints)) {
                    _animator.SetTrigger("Yell"); // Используем триггер для анимации "yelling"
                    FindObjectOfType<AudioManager>().Play("Scream");
                }
            }
        } 
    }



    // Переменная, вызывающаяся в аниматоре
    // Не используется
    public void EnableMovement()
    {
        _canMove = true;
    }

    // Метод для проверки, проигрывается ли анимация
    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        bool isPlaying = stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f;
        return isPlaying;
    }
}
