using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTree : MonoBehaviour
{
    [SerializeField] private float _treeHealth;
    private float _treeMaxHealth = 100f;

    private GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        _treeHealth = _treeMaxHealth;

        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerController player = _player.GetComponent<PlayerController>();
                _treeHealth -= player.Damage;
            }
            if(_treeHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
