using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTree : MonoBehaviour
{
    [SerializeField] private float _treeHealth;
    private float _treeMaxHealth = 100f;

    private PlayerController _player;

    private bool _isPlayerStandingNextToMe = false;

    void Start()
    {
        _treeHealth = _treeMaxHealth;

        _player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        Cut();
    }

    private void Cut()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _treeHealth -= _player.Damage;
        }
        if (_treeHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerStandingNextToMe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _isPlayerStandingNextToMe = false;
        }
    }
}
