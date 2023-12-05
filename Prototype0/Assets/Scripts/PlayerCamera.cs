using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float x_Sensitivity;
    public float y_Sensitivity;

    public Transform Orientation;

    float x_Rotation;
    float y_Rotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    void Update()
    {
        //mouse input data
        float mouse_X = Input.GetAxisRaw("Mouse X") * Time.deltaTime * x_Sensitivity;
        float mouse_Y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * y_Sensitivity;

        y_Rotation += mouse_X;
        x_Rotation -= mouse_Y;

        x_Rotation = Mathf.Clamp(x_Rotation, -90f, 90f);

        //rotate / orient 
        transform.rotation = Quaternion.Euler(x_Rotation, y_Rotation, 0);
        Orientation.rotation = Quaternion.Euler(0, y_Rotation, 0);
        


    }
}
