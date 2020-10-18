using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject _playerSpawn;

    [SerializeField] private float _movementSpeed;
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
    private bool _isCrouching = false;
    private bool _isSprinting = false;
    private float _damage = 15f;
    private PlayerStats _playerStats;

    [SerializeField] private Inventory _inventory;
    private InventoryHandler _inventoryHandler;

    private bool _isNearAnItem = false;
    private Item _itemNearBy = null;

    public float Damage
    {
        get { return _damage; }
    }

    [SerializeField] private float _swimSpeed;

    void Start()
    {
        _inventoryHandler = new InventoryHandler();

        _rigidbody = GetComponent<Rigidbody>();

        _playerSpawn = GameObject.Find("PlayerSpawn");

        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Crouch();
        Climb();
        Swim();
        Dead();
        PickUpItem();
    }

    private void PickUpItem()
    {
        if(_isNearAnItem)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _itemNearBy.GetComponent<Item>().Object = _itemNearBy.gameObject;
                _inventory.AddItem(_itemNearBy);
                _itemNearBy.gameObject.SetActive(false);
            }
        }
    }

    private void Dead()
    {
        if (_playerStats.Health <= 0)
        {
            print("You have died!");
            _rigidbody.gameObject.transform.position = this._playerSpawn.transform.position;
        }
    }

    private void Swim()
    {
        if (_rigidbody.transform.position.y <= 0.7)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Input.GetAxis("Jump") * _swimSpeed, _rigidbody.velocity.z);
        }
    }

    private void Climb()
    {
        _wallType = Physics.CheckSphere(_wallCheck.transform.position, _wallRadius, _wall);

        if (_wallType)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Input.GetAxis("Climb") * _climbSpeed, _rigidbody.velocity.z);
        }
    }

    private void Crouch()
    {
        _isCrouching = Input.GetAxisRaw("Crouch") != 0;

        if (_isCrouching)
        {
            _movementSpeed = 5;
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.transform.position, _groundRadius, _ground);

        if (_isGrounded)
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerStats.DecreaseStamina();
            if (_playerStats.Stamina > 0)
            {
                _movementSpeed = 20f;
            }
            else
            {
                _movementSpeed = 10f;
            }
        }
        else
        {
            _movementSpeed = 10f;
            if(_playerStats.Stamina < 100)
            {
                _playerStats.IncreaseStamina();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            _isNearAnItem = true;
            _itemNearBy = other.GetComponent<Item>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            _isNearAnItem = false;
            _itemNearBy = null;
        }
    }
}
