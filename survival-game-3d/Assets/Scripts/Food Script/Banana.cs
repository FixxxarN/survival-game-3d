using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private float _hungerIncreaseValue = 20f;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats Player = other.GetComponent<PlayerStats>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.IncreaseHunger(_hungerIncreaseValue);
                Destroy(gameObject);
            }
        }

    }
}
