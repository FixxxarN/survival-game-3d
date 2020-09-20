using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _playerMovement;
    private Rigidbody _rigidbody;


    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _groundRadius;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private bool _isGrounded;

    [SerializeField] private GameObject _wallCheck;
    [SerializeField] private LayerMask _wall;
    [SerializeField] private float _wallRadius;
    [SerializeField] private float _climbSpeed;
    [SerializeField] private bool _wallType;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Climb();
    }

    private void Climb()
    {
        _wallType = Physics.CheckSphere(_wallCheck.transform.position, _wallRadius, _wall);

        if(_wallType)
        {
            if(Input.GetKey(KeyCode.F))
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Input.GetAxis("Climb") * _climbSpeed, _rigidbody.velocity.z);
            }
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
        float x = Input.GetAxisRaw("Horizontal") * _playerMovement * Time.deltaTime;
        float z = Input.GetAxisRaw("Vertical") * _playerMovement * Time.deltaTime;

        Vector3 desiredPosition = transform.position + (transform.right * x + transform.forward * z);

        _rigidbody.MovePosition(desiredPosition);
    }
}
