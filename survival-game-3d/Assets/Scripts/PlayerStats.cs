using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _stamina = 100f;
    [SerializeField] private float _hunger = 100f;
    [SerializeField] private float _thirst = 100f;

    private float _maxHealth = 100f;
    private float _maxStamina = 100f;
    private float _maxHunger = 100f;
    private float _maxThirst = 100f;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _hungerBar;
    [SerializeField] private Image _thirstBar;
    [SerializeField] private Image _staminaBar;

    public float Health
    {
        get { return _health; }
    }

    public float Stamina
    {
        get { return _stamina; }
    }

    public float Hunger
    {
        get { return _hunger; }
    }

    public float Thirst
    {
        get { return _thirst; }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DecreaseHunger", 5f, 35f);
        InvokeRepeating("DecreaseThirst", 5f, 12f);
        InvokeRepeating("IncreaseHealth", 5f, 5f);
    }

    void Update()
    {
        UpdateUIStats();
    }

    private void UpdateUIStats()
    {
        _healthBar.rectTransform.localScale = new Vector3(_health / 100, 1, 1);
        _hungerBar.rectTransform.localScale = new Vector3(_hunger / 100, 1, 1);
        _thirstBar.rectTransform.localScale = new Vector3(_thirst / 100, 1, 1);
        _staminaBar.rectTransform.localScale = new Vector3(_stamina / 100, 1, 1);
    }

    private void DecreaseHunger()
    {
        _hunger -= 2.3f;
    }

    private void DecreaseThirst()
    {
        _thirst -= 1.5f;
    }

    public void IncreaseHunger(float hunger)
    {
        if (_hunger < _maxHunger)
        {
            _hunger += hunger;
            if (_hunger >= _maxHunger)
            {
                _hunger = _maxHunger;
            }
        }
    }

    private void IncreaseHealth()
    {
        if (_health <= _maxHealth)
        {
            _health += 5f;
            if (_health >= _maxHealth)
            {
                _health = _maxHealth;
            }
        }
        else
        {
            print("You have full health");
        }
    }

    public void IncreaseThirst(float thirst)
    {
        if (_thirst < _maxThirst)
        {
            _thirst += thirst;
            if (_thirst >= _maxThirst)
            {
                _thirst = _maxThirst;
            }
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L)) // testing purposes for health
    //    {
    //        _health -= 7f;
    //    }
    //}
}
