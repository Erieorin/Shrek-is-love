using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController _characterController;
    public Transform _camera;

    public float _speed = 6f;

    // плавное движение камеры
    public float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    void Update()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");
        Vector3 _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if (_direction.magnitude >= 0.1f) // проверяем, двиагемся ли, сравнивая длину вектора направления
        {
            // угол поворота игрока. atan2 - функция, возвращающая угол между осью и вектором направления
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y; // переводим в гарудсы и добавляем вращение камеры по y 
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, _angle, 0f);

            Vector3 _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _characterController.Move(_moveDirection.normalized * _speed * Time.deltaTime);
        }
    }
}
