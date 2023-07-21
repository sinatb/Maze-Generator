using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    float rotationX = 0;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public Camera playerCamera;
    public GameObject light;
    private Light _light;

    // Update is called once per frame
    private void Start()
    {
        _light = light.GetComponent<Light>();
    }

    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        var deltaz = Input.GetAxis("Vertical");
        var deltax = Input.GetAxis("Horizontal");
        float currSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currSpeed = runSpeed;
        }
        else
            currSpeed = speed;
        if (Input.GetKeyUp(KeyCode.F))
        {
            _light.enabled = !_light.enabled;
        }
        transform.Translate(Vector3.forward*(Time.deltaTime*deltaz*currSpeed));
        transform.Translate(Vector3.right*(Time.deltaTime*deltax*currSpeed));
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        light.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
