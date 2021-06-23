using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _sprintMultiplier = 2f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _gravityMultiplier = 2f;

    private float _oldSpeed;
    
    private readonly float GRAVITY_CONSTANT = -9.81f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _velocity;
    
    private bool _isGrounded;
    private bool _isSprinting;
    private bool _isStunned = false;
    
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
        if (_isStunned)
            return;
        Movement();
        CheckInput();
    }

    private void ApplyGravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;
        
        _velocity.y += GRAVITY_CONSTANT * _gravityMultiplier * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

    }
    
    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (_isSprinting)
            _speed = Mathf.Lerp(_speed, _oldSpeed * _sprintMultiplier, 0.1f);

        Vector3 moveDir = transform.right * x + transform.forward * z;

        _characterController.Move(moveDir * (_speed * Time.deltaTime));
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
        if(Input.GetKeyDown(KeyCode.LeftShift))
            Sprint();
        if(Input.GetKeyUp(KeyCode.LeftShift))
            EndSprint();
        //TODO: crouching
    }

    private void Jump()
    {
        if (!_isGrounded)
            return;
        _velocity.y = Mathf.Sqrt(_jumpForce * -2f * GRAVITY_CONSTANT * _gravityMultiplier);
    }

    private void Sprint()
    {
        if (!_isGrounded)
                return;
        _oldSpeed = _speed;
        _isSprinting = true;
    }

    private void EndSprint()
    {
        _speed = _oldSpeed;
        _isSprinting = false;
    }

    public IEnumerable ChangeSpeed(float pertentage, float duration = 1f)
    {
        float old = _speed;
        if (_isSprinting)
            old = _oldSpeed;
        _speed = _speed * pertentage;
        yield return new WaitForSeconds(duration);
        _speed = old;
    }

    public IEnumerable Stun(float duration)
    {
        _isStunned = true;
        //PlayAnim;
        yield return new WaitForSeconds(duration);
        _isStunned = false;

    }
    
}
