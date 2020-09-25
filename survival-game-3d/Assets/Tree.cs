using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fruits;
    [SerializeField] private int _amountOfFruits;
    [SerializeField] private GameObject _fruit;
    [SerializeField] private float _chanceOfGettingFruit;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject fruit = (GameObject)Instantiate(_fruit, new Vector3(0, 0, 0), Quaternion.identity);
            _fruits.Add(fruit);
        }
        for (int i = 0; i < _fruits.Count; i++)
        {
            _fruits[i].transform.position = transform.GetChild(i).transform.position;
            Destroy(transform.GetChild(i).gameObject);
            _fruits[i].transform.SetParent(this.transform);
        }
        //for(int i = 0; i < _fruits.Count; i++)
        //{
        //    //bool negativeValue = Random.Range(0, 2) > 0 ? true : false; 
        //    //GameObject fruit = (GameObject)Instantiate(_fruits[i], this.transform.position, Quaternion.identity, this.transform);
        //    //fruit.transform.localScale = new Vector3(1, 0.2f, 1);
        //    //fruit.transform.localPosition = new Vector3(
        //    //    negativeValue ? -Random.Range(1, 3) : Random.Range(1, 3),
        //    //    1,
        //    //    negativeValue ? -Random.Range(1, 3) : Random.Range(1, 3)
        //    //    );
        //    //fruit.GetComponent<Rigidbody>().isKinematic = true;
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(Random.Range(0f, 1f) < _chanceOfGettingFruit)
                {
                    int randomNumber = Random.Range(0, _fruits.Count - 1);
                    Transform fruit = transform.GetChild(randomNumber);
                    fruit.GetComponent<Rigidbody>().isKinematic = false;
                    fruit.parent = null;
                    _fruits.RemoveAt(randomNumber);
                }
            }
        }
    }
}
