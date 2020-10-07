using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{
    [SerializeField] private float _hungerValue;
    [SerializeField] private float _thirstValue;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
                if (_hungerValue != 0)
                {
                    playerStats.IncreaseHunger(_hungerValue);
                }
                if (_thirstValue != 0)
                {
                    playerStats.IncreaseThirst(_thirstValue);
                }
                Destroy(gameObject);
            }
        }
    }
}
