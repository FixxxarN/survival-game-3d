using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Sensitivity = 300f;
    public GameObject _player;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        RotateMouse();
    }

    private void RotateMouse()
    {
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _player.transform.Rotate(Vector3.up * MouseX);
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.5f, _player.transform.position.z);
    }
}
