using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform Player;
    float MouseX;
    float MouseY;
    public float mouseSensitivity = 100f;
    float xRotation;
    public Transform gun;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        MouseX = Input.GetAxis("Mouse X") * mouseSensitivity ;
        MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity ;
       
    }

    private void FixedUpdate()
    {
        xRotation -= MouseY ;
        xRotation = Mathf.Clamp(xRotation, -89, 89);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * MouseX);
        gun.forward = -transform.forward;

    }
}
