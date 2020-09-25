using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : MonoBehaviour
{
    private float _hungerIncreaseValue = 20f;
    private float _thirstIncreasevalue = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats Player = other.GetComponent<PlayerStats>();
            if(Input.GetKeyDown(KeyCode.E))
            {
                Player.IncreaseHunger(_hungerIncreaseValue);
                Player.IncreaseThirst(_thirstIncreasevalue);
                Destroy(gameObject);
            }
        }

    }
}
