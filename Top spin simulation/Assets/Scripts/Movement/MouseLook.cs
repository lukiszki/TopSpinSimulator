using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    [Range(0f,100f)]
    float mouseSensitivity = 100f;

    [SerializeField]
    Transform playerBody;

    private float xRotation;

    Gyroscope gyro;

    Quaternion r;

    Vector2 mousePos,mouseDelta;
    Vector3 rotar, rotarb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
       // Input.gyro.enabled = true;
    }
    private  Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x-r.x, q.y-r.y, -q.z-r.z, -q.w-r.w);
    }
    public void Reset()
    {
        r = transform.rotation;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotarb = playerBody.localRotation.eulerAngles;
            rotar = transform.localRotation.eulerAngles;
            mousePos = Input.mousePosition;
            mousePos.x /= Screen.width;
            mousePos.y /= Screen.height;
        }
        if (Input.GetMouseButton(0))
        {
            mouseDelta = Input.mousePosition;
            mouseDelta.x /= Screen.width;
            mouseDelta.y /= Screen.height;
            mouseDelta = mousePos - mouseDelta;
            //print(mouseDelta);
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //print(GyroToUnity(Input.gyro.attitude));
        //transform.rotation = GyroToUnity(Input.gyro.attitude);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90f);
        //transform.localRotation = Quaternion.Euler(Input.gyro.rotationRate.x,0,0);
          transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.localRotation = Quaternion.Euler(rotar + new Vector3(-mouseDelta.y, 0, 0)*36);
        //playerBody.localRotation = Quaternion.Euler(rotarb + new Vector3(0, mouseDelta.x, 0) * 36);
        playerBody.Rotate(mouseX * Vector3.up);
    }
}
