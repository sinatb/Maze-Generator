using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float rotationX = 0;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        var deltaz = Input.GetAxis("Vertical");
        var deltax = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward*(Time.deltaTime*deltaz*speed));
        transform.Translate(Vector3.right*(Time.deltaTime*deltax*speed));
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
