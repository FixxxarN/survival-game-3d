using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _playerMovement;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal") * _playerMovement * Time.deltaTime;
        float z = Input.GetAxisRaw("Vertical") * _playerMovement * Time.deltaTime;

        Vector3 desiredPosition = transform.position + (transform.right * x + transform.forward * z);

        _rigidbody.MovePosition(desiredPosition);
    }
}
