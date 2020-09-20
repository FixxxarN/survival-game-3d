using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _groundRadius;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private bool _isGrounded;

    private bool _isCrouching = false;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Crouch();
    }

    private void Crouch()
    {
        _isCrouching = Input.GetAxisRaw("Crouch") != 0;

        if(_isCrouching)
        {
            _movementSpeed = 5;
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            _movementSpeed = 10;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.transform.position, _groundRadius, _ground);
        
        if(_isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Input.GetAxis("Jump") * _jumpHeight, _rigidbody.velocity.z);
        }
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal") * _movementSpeed * Time.deltaTime;
        float z = Input.GetAxisRaw("Vertical") * _movementSpeed * Time.deltaTime;

        Vector3 desiredPosition = transform.position + (transform.right * x + transform.forward * z);

        _rigidbody.MovePosition(desiredPosition);
    }
}
