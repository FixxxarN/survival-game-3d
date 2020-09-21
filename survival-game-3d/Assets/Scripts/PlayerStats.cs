using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float _health = 100f;
    private float _stamina = 100f;
    private float _hunger = 100f;
    private float _thirst = 100f;

    public float Health {
        get { return _health; }
    }

    public float Stamina { 
        get { return _stamina; }
    }
    
    public float Hunger { 
        get { return _hunger; }
    }

    public float Thirst { 
        get { return _thirst; }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("decreaseHunger", 5f, 35f);
        InvokeRepeating("decreaseThirst", 5f, 12f);
    }
    
    private void decreaseHunger()
    {
        _hunger -= 2.3f;
    }

    private void decreaseThirst()
    {
        _thirst -= 1.5f;
    }
}
